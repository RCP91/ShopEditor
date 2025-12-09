/*
 * Developed by: RCP91 (Roberto Carlos Preguica)
 * Program for editing shops on Tibia servers and clients.
 * Specifically for Tibia Project Cherry.
 * Opens, edits, and saves shop list files for server/client using Lua.
 * On the client side, item/clothing type IDs are saved for correct loading.
 * MIT License.
 */

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ShopEditor
{
    public partial class F_Main : Form
    {
        public List<string> categoryTypes = new List<string>();
        public List<string> offerTypes = new List<string>();
        ShopManager shopEditor;

        public static F_Main Instance;
        public F_Main()
        {
            Config.Load();
            InitializeComponent();
            Instance = this;

            shopEditor = new ShopManager();

            progressBar1.Visible = false;
            tb_pathServer.Text = Config.Current.ServerFilePath;
            tb_pathClient.Text = Config.Current.ClientFilePath;
            tb_pathItemServer.Text = Config.Current.PathItemServer;
            LoadShopItemsServer();

        }
        async void LoadShopItemsServer(bool force = false)
        {
            tb_display.Text = "";
            await RunProgress(progressBar1);
            if (!force || shopEditor.ItemNameToId == null)
            {
                if (tb_pathItemServer.Text == string.Empty) return;

                Msg.Log($":: Loading items.srv!");
                shopEditor.LoadItemServer(tb_pathItemServer.Text);
            }
            if (Config.Current.AutoLoadLastFile || force)
            {
                if (!File.Exists(Config.Current.PathItemServer))
                {
                    Msg.Error("items.srv file not found! Please configure the correct path in the configuration menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string path = (force) ? tb_pathServer.Text : Config.Current.ServerFilePath;
                if (path != string.Empty) {
                    Msg.Log($":: Loading shop items!");
                    shopEditor.LoadShopServer(path);
                }
                dgv_load();
            }
        }
        public async Task RunProgress(System.Windows.Forms.ProgressBar bar)
        {
            bar.Visible = true;
            bar.Maximum = 20;

            int stap = 0;

            for (bar.Value = 1; bar.Value < bar.Maximum; bar.Value++)
            {
                if (bar.Value != stap)
                {
                    stap = bar.Value;
                    await Task.Delay(10);
                }
            }

            var t = new Timer { Interval = 750 };
            t.Tick += (_, __) =>
            {
                bar.Visible = false;
                t.Stop();
            };
            t.Start();
        }

        void dgv_load()
        {
            dgv_main.Rows.Clear();

            if (shopEditor?.Categories == null) return;

            (dgv_main.Columns["dgv_cb_category"] as DataGridViewComboBoxColumn).Items.AddRange(categoryTypes.ToArray());
            //(dgv_main.Columns["dgv_cb_offerType"] as DataGridViewComboBoxColumn).Items.AddRange(offerTypes.ToArray());

            foreach (var cat in shopEditor.Categories)
            {
                foreach (var offer in cat.Offers)
                {
                    //var items = shopEditor.ItemNameToId.FirstOrDefault(f => f.Value.ToString() == ).Key;
                    dgv_main.Rows.Add(
                        cat.CategoryName,
                        cat.CategoryIcoName ?? cat.CategoryIcoId.ToString(),
                        (offer.LookType > 0) ? offer.LookType.ToString() : offer.Names,
                        offer.Price,
                        offer.Sex.HasValue ? (offer.Sex == 0 ? "Female" : "Male") : (offer.LookType > 0 ? "" : offer.Count.ToString()),
                        offer.Desc ?? "",
                        offer.Title ?? ""
                    );
                }
            }
        }
        private void SaveDgvToShopEditor()
        {
            shopEditor.Categories.Clear();

            var rows = dgv_main.Rows
                .Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow && r.Cells["dgv_cb_category"].Value != null)
                .ToList();

            if (!rows.Any()) return;

            var grouped = rows.GroupBy(r => new
            {
                Category = r.Cells["dgv_cb_category"].Value?.ToString() ?? "Unknown",
                Ico = r.Cells["dgv_ico_type"].Value?.ToString() ?? ""
            });

            int order = 1;
            foreach (var g in grouped)
            {
                var category = new Category
                {
                    Order = order++,
                    CategoryName = g.Key.Category,
                    CategoryIcoName = g.Key.Ico
                };

                var icoItem = shopEditor.ItemNameToId.FirstOrDefault(r =>
                    r.Key.Equals(g.Key.Ico, StringComparison.OrdinalIgnoreCase));

                if (icoItem.Key != null)
                {
                    category.CategoryIcoId = icoItem.Value;
                }
                else
                {
                    category.CategoryIcoId = (int)Convert.ToInt16(g.Key.Ico);
                }

                foreach (var row in g)
                {
                    var itemLook = row.Cells["dgv_item_look"].Value?.ToString() ?? "";
                    var countSexValue = row.Cells["dgv_count_sex"].Value?.ToString() ?? "";
                    var price = Convert.ToInt32(row.Cells["dgv_price"].Value ?? 0);
                    var desc = row.Cells["dgv_desc"].Value?.ToString() ?? "";
                    var title = row.Cells["dgv_title"].Value?.ToString() ?? "";

                    bool isOutfit = int.TryParse(itemLook, out int lookTypeValue) && lookTypeValue > 0;

                    var offer = new Offers();

                    if (isOutfit)
                    {
                        offer.LookType = lookTypeValue;
                        offer.Title = title;
                        offer.Price = price;

                        if (!string.IsNullOrEmpty(countSexValue))
                        {
                            switch (countSexValue.ToLower())
                            {
                                case "female":
                                case "0":
                                    offer.Sex = 0;
                                    break;
                                case "male":
                                case "1":
                                    offer.Sex = 1;
                                    break;
                                default:
                                    offer.Sex = null;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        offer.Names = itemLook;
                        offer.Price = price;
                        offer.Desc = desc;

                        if (int.TryParse(countSexValue, out int countValue))
                        {
                            offer.Count = countValue;
                        }
                        else
                        {
                            offer.Count = 1;
                        }

                        var foundItem = shopEditor.ItemNameToId.FirstOrDefault(r =>
                            r.Key.Equals(itemLook, StringComparison.OrdinalIgnoreCase));

                        if (foundItem.Key != null)
                        {
                            offer.ItemId = foundItem.Value;
                        }
                        else
                        {
                            Msg.Warning($"Item '{itemLook}' not found in items.srv!");
                        }
                    }

                    category.Offers.Add(offer);
                }

                shopEditor.Categories.Add(category);
            }

            shopEditor.Categories.Sort((a, b) => a.Order.CompareTo(b.Order));
        }
        private void dgv_main_MouseLeave(object sender, EventArgs e)
        {
            if (pn_config.Visible && !pn_config.ClientRectangle.Contains(pn_config.PointToClient(Cursor.Position)))
            {
                if (cb_autoSave.Checked)
                {
                    //SaveDgvToShopEditor();
                }
            }
        }
        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgv_main.Enabled = false;
            pn_config.Visible = true;

        }
        private void pn_config_MouseLeave(object sender, EventArgs e)
        {
            if (!pn_config.ClientRectangle.Contains(pn_config.PointToClient(Cursor.Position)))
            {
                pn_config.Visible = false;
                dgv_main.Enabled = true;

                Config.Update(cfg =>
                {
                    cfg.ServerFilePath = tb_pathServer.Text;
                    cfg.ClientFilePath = tb_pathClient.Text;
                    cfg.PathItemServer = tb_pathItemServer.Text;
                    cfg.AutoLoadLastFile = cb_autoLoad.Checked;
                    cfg.AutoSave = cb_autoSave.Checked;
                    cfg.CategoryList = categoryTypes;
                });
            }
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            if (cb_category.Text != string.Empty)
            {
                if (string.IsNullOrWhiteSpace(cb_category.Text))
                {
                    Msg.Warning("Category type cannot be space!", "Refused!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (categoryTypes.Contains(cb_category.Text))
                {
                    Msg.Warning("Category type already exists!", "Refused!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                categoryTypes.Add(cb_category.Text.ToLower());
                cb_category.Items.Clear();
                cb_category.Items.AddRange(categoryTypes.ToArray());
                cb_category.Text = "";
            }

        }
        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (cb_category.SelectedItem != null)
            {
                categoryTypes.Remove(cb_category.SelectedItem.ToString());
                cb_category.Items.Clear();
                cb_category.Items.AddRange(categoryTypes.ToArray());
                cb_category.Text = "";
            }
        }
        private void cb_autoSave_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void cb_autoLoad_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void tb_pathServer_MouseClick(object sender, MouseEventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                tb_pathServer.Text = openFileDialog1.FileName;
            }
        }
        private void tb_pathClient_MouseClick(object sender, MouseEventArgs e)
        {
            DialogResult res = folderBrowserDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                tb_pathClient.Text = folderBrowserDialog1.SelectedPath;
            }
        }
        private void tb_pathItemServer_DoubleClick(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();
            Msg.Log(":: Select items.srv file path");
            if (res == DialogResult.OK)
            {
                tb_pathItemServer.Text = openFileDialog1.FileName;
            }
        }
        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool pathOk1 = false;
            bool pathOk2 = false;
            Msg.Log(":: Replace or save like file shop_items.lua");
            DialogResult serv = saveFileDialog1.ShowDialog();
            if (serv == DialogResult.OK)
            {
                tb_pathServer.Text = saveFileDialog1.FileName;
                pathOk1 = true;
            }

            Msg.Log(":: Replace or save like file shop_items_client.lua");
            DialogResult client = saveFileDialog1.ShowDialog();
            if (client == DialogResult.OK)
            {
                tb_pathClient.Text = saveFileDialog1.FileName;
                pathOk2 = true;
            }
            if (pathOk1 && pathOk2)
            {
                SaveDgvToShopEditor();
                if (shopEditor.Save(tb_pathServer.Text, tb_pathClient.Text))
                {
                    Msg.Log("Files saved successfully!");
                }
            }
        }
        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tb_pathServer_MouseClick(sender, null);
            LoadShopItemsServer(true);
        }
    }
    class Msg
    {
        public static string endl = System.Environment.NewLine;
        public static void Warning(string message, string caption = "Shop Editor Pro", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Warning)
        {
            MessageBox.Show(message, caption, buttons, icon);
        }
        public static DialogResult Question(string message, string caption = "Shop Editor Pro", MessageBoxButtons buttons = MessageBoxButtons.YesNo, MessageBoxIcon icon = MessageBoxIcon.Question)
        {
            return MessageBox.Show(message, caption, buttons, icon);
        }
        public static void Error(string message, string caption = "Shop Editor Pro", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Error)
        {
            MessageBox.Show(message, caption, buttons, icon);
        }
        public static DialogResult Info(string message, string caption = "Shop Editor Pro", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            return MessageBox.Show(message, caption, buttons, icon);
        }
        public static void Log(string message)
        {
            var instance = F_Main.Instance;
            instance.tb_display.Text += message + endl;
            instance.tb_display.SelectionStart = instance.tb_display.Text.Length;
            instance.tb_display.ScrollToCaret();
        }
    }
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new F_Main());
        }
    }
}
