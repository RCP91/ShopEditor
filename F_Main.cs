/*
 * Developed by: RCP91 (Roberto Carlos Preguica)
 * Program for editing shops on Tibia servers and clients.
 * Specifically for Tibia Project Cherry.
 * Opens, edits, and saves shop list files for server/client using Lua.
 * On the client side, item/clothing type IDs are saved for correct loading.
 * MIT License.
 */

using Newtonsoft.Json.Linq;
using ShopEditorPro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ShopEditor
{
    public partial class F_Main : Form
    {
        List<string> offerTypes = new List<string>();
        ShopEditorPro.ShopEditorPro shopEditor = new ShopEditorPro.ShopEditorPro();

        public static F_Main Instance;
        public F_Main()
        {
            Config.Load();
            InitializeComponent();
            Instance = this;

            shopEditor = new ShopEditorPro.ShopEditorPro();

            tb_pathServer.Text = Config.Current.ServerFilePath;
            tb_pathClient.Text = Config.Current.ClientFilePath;
            tb_pathItemServer.Text = Config.Current.PathItemServer;

            offerTypes = new List<string>(Config.Current.OfferTypeList ?? new List<string> { "Item", "Outfit" });
            cb_offerType.Items.Clear();
            cb_offerType.Items.AddRange(offerTypes.ToArray());
            dgv_load();

            if (!File.Exists(Config.Current.PathItemServer))
            {
                Msg.Error("items.srv file not found! Please configure the correct path in the configuration menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadShopItemsServer();
        }
        async void LoadShopItemsServer(bool force = false)
        {
            tb_display.Text = "";
            Msg.Log($":: Loading items.srv!");

            await RunProgress(progressBar1);
            if (!force)
            {
                shopEditor.LoadItemServer(tb_pathItemServer.Text);
            }

            if (Config.Current.AutoLoadLastFile || force)
            {
                Msg.Log($":: Loading shop items!");
                string path = (force) ? tb_pathServer.Text : Config.Current.ServerFilePath;
                shopEditor.LoadShopServer(path);

                dgv_load();

                foreach (var item in shopEditor.Categories.ToList())
                {
                    foreach (var outfit in item.Outfits)
                        Msg.Log($"::: {outfit.Title}::Type:{outfit.LookType}");

                    foreach (var it in item.Items)
                        Msg.Log($"::: {it.Name}::ID: {it.ItemId}");
                }
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

            var t = new Timer { Interval = 1500 };
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

            //(dgv_main.Columns["dgv_cb_category"] as DataGridViewComboBoxColumn).Items.Clear();
            var categoryNames = shopEditor.Categories.Select(c => c.Name).ToArray();
            (dgv_main.Columns["dgv_cb_category"] as DataGridViewComboBoxColumn).Items.AddRange(categoryNames);

            //(dgv_main.Columns["dgv_cb_offerType"] as DataGridViewComboBoxColumn).Items.Clear();
            (dgv_main.Columns["dgv_cb_offerType"] as DataGridViewComboBoxColumn).Items.AddRange(offerTypes.ToArray());


            foreach (var cat in shopEditor.Categories)
            {
                if (cat.IsOutfit)
                {
                    foreach (var outfit in cat.Outfits)
                    {
                        dgv_main.Rows.Add(
                            cat.Name,
                            "outfits",
                            cat.LookIco,
                            outfit.LookType,
                            outfit.Price,
                            outfit.Sex.HasValue ? (outfit.Sex == 0 ? "Female" : "Male") : "Both",
                            "",
                            outfit.Title ?? ""
                        );
                    }
                }
                else if (cat.Items.Count > 0)
                {
                    foreach (var item in cat.Items)
                    {
                        dgv_main.Rows.Add(
                            cat.Name,
                            "items",
                            cat.IconItemName,
                            item.Name,
                            item.Price,
                            item.Count,
                            item.Desc ?? "",
                            ""
                        );
                    }
                }
            }
        }
        void SaveDgvToShopEditor()
        {
            shopEditor.Categories.Clear();
            var grouped = dgv_main.Rows
                .Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow && r.Cells["dgv_cb_category"].Value != null)
                .GroupBy(r => new
                {
                    Category = r.Cells["dgv_cb_category"].Value?.ToString(),
                    OfferType = r.Cells["dgv_cb_offerType"].Value?.ToString()?.ToLower()
                });

            foreach (var group in grouped)
            {
                string catName = group.Key.Category;
                bool isOutfit = group.Key.OfferType == "outfits";

                var category = new ShopCategory
                {
                    Order = shopEditor.Categories.Count + 1,
                    Name = catName,
                    //IsOutfit = isOutfit
                };

                if (isOutfit)
                {
                    foreach (var row in group)
                    {
                        category.LookIco = Convert.ToUInt16(row.Cells["dgv_ico_type"].Value);
                        var outfit = new ShopOutfit
                        {
                            LookType = Convert.ToInt32(row.Cells["dgv_item_look"].Value),
                            Price = Convert.ToInt32(row.Cells["dgv_price"].Value),
                            Title = row.Cells["dgv_title"].Value?.ToString() ?? ""
                        };
                        string sexStr = row.Cells["dgv_count_sex"].Value?.ToString()?.ToLower();
                        outfit.Sex = (sexStr == "female") ? 0 : ((sexStr == "male") ? 1 : outfit.Sex = null);
                        category.Outfits.Add(outfit);
                    }
                }
                else if (!isOutfit)
                {
                    category.IconItemName = dgv_ico_type.ToString();

                    foreach (var row in group)
                    {
                        var item = new ShopItem
                        {
                            Name = row.Cells["dgv_item_look"].Value?.ToString() ?? "unknown",
                            Price = Convert.ToInt32(row.Cells["dgv_price"].Value),
                            Count = Convert.ToInt32(row.Cells["dgv_count_sex"].Value),
                            Desc = row.Cells["dgv_desc"].Value?.ToString() ?? ""
                        };

                        if (shopEditor.ItemNameToId.TryGetValue(item.Name, out int id))
                            item.ItemId = id;

                        category.Items.Add(item);
                    }
                }
                shopEditor.Categories.Add(category);
            }
            shopEditor.ResolveItemId();
        }
        private void dgv_main_MouseLeave(object sender, EventArgs e)
        {
            if (pn_config.Visible && !pn_config.ClientRectangle.Contains(pn_config.PointToClient(Cursor.Position)))
            {
                if (cb_autoSave.Checked)
                {
                    SaveDgvToShopEditor();
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
                    cfg.OfferTypeList = offerTypes;
                });
            }
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cb_offerType.Text))
            {
                Msg.Warning("Offer type cannot be empty!", "Refused!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (offerTypes.Contains(cb_offerType.Text))
            {
                Msg.Warning("Offer type already exists!", "Refused!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            offerTypes.Add(cb_offerType.Text);
            cb_offerType.Items.Clear();
            cb_offerType.Items.AddRange(offerTypes.ToArray());
            cb_offerType.Text = "";
        }
        private void btn_remove_Click(object sender, EventArgs e)
        {
            offerTypes.Remove(cb_offerType.SelectedItem.ToString());
            cb_offerType.Items.Clear();
            cb_offerType.Items.AddRange(offerTypes.ToArray());
            cb_offerType.Text = "";
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
