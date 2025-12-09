using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ShopEditor
{
    public class Offers
    {
        public string Names { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public string Desc { get; set; }
        public int LookType { get; set; }
        public int? Sex { get; set; }
        public string Title { get; set; }
        public int ItemId { get; set; }
    }
    public class Category
    {
        public int Order { get; set; }
        public string CategoryName { get; set; }
        public string CategoryIcoName { get; set; }
        public int CategoryIcoId { get; set; }
        public List<Offers> Offers { get; set; } = new List<Offers>();
    }

    public class ShopManager
    {
        public List<Category> Categories { get; } = new List<Category>();
        public Dictionary<string, int> ItemNameToId = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        public async void SaveItemsBin(string path)
        {
            Msg.Log(":: creating items.bin...");
            using (var fs = new FileStream(path, FileMode.Create))
            using (var bw = new BinaryWriter(fs))
            {
                bw.Write(ItemNameToId.Count);

                foreach (var kvp in ItemNameToId)
                {
                    bw.Write(kvp.Key);
                    bw.Write(kvp.Value);
                }
            }
        }

        public async void LoadItemsBin(string path)
        {
            ItemNameToId.Clear();

            using (var fs = new FileStream(path, FileMode.Open))
            using (var br = new BinaryReader(fs))
            {
                var count = br.ReadInt32();

                for (int i = 0; i < count; i++)
                {
                    var key = br.ReadString();
                    var val = br.ReadInt32();
                    ItemNameToId[key.ToLower()] = val;
                }
            }
        }

        public void LoadItemServer(string srvPath)
        {
            var binPath = Path.ChangeExtension(srvPath, ".bin");

            if (File.Exists(binPath))
            {
                LoadItemsBin(binPath);
                Msg.Log(":: items.bin fast loading...");
                return;
            }
            ParseItemsSrv(srvPath);
            SaveItemsBin(binPath);
        }

        private void ParseItemsSrv(string itemsSrvPath)
        {
            ItemNameToId.Clear();
            var lines = File.ReadAllLines(itemsSrvPath);

            string currentName = "";
            int currentId = 0;

            foreach (var line in lines)
            {
                var l = line.Trim();

                bool takeFlag = true;

                var idMatch = Regex.Match(l, @"TypeID\s*=\s*(\d+)");
                if (idMatch.Success)
                    currentId = int.Parse(idMatch.Groups[1].Value);

                var nameMatch = Regex.Match(l, @"Name\s*=\s*""([^""]+)""");

                if (l.Contains("Flags"))
                {
                    var noPassed = new List<string> { "Unmove", "Unlay", "Door", };
                    takeFlag = !noPassed.Any(v => l.Contains(v));
                }

                if (nameMatch.Success)
                {
                    currentName = nameMatch.Groups[1].Value.Trim();
                    var blocked = currentName.Contains("dead");

                    if (currentId > 0 && !string.IsNullOrEmpty(currentName) && takeFlag && !blocked && !ItemNameToId.ContainsKey(currentName))
                    {
                        var cleanName = currentName;

                        if (cleanName.StartsWith("a ", StringComparison.OrdinalIgnoreCase))
                            cleanName = cleanName.Substring(2);

                        if (cleanName.StartsWith("an ", StringComparison.OrdinalIgnoreCase))
                            cleanName = cleanName.Substring(3);

                        cleanName = cleanName.Trim().ToLower();

                        ItemNameToId[cleanName] = currentId;
                    }
                }
            }
        }

        private void ParseServerLua(string lua)
        {
            var lines = lua.Split('\n');
            Category c = null;
            var instance = F_Main.Instance;

            foreach (var raw in lines)
            {
                var line = raw.Trim();
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("--")) continue;

                var catMatch = Regex.Match(line, @"^\[(\d+)\]\s*=\s*\{");
                if (catMatch.Success)
                {
                    if (c != null) Categories.Add(c);
                    c = new Category { Order = int.Parse(catMatch.Groups[1].Value) };
                    continue;
                }

                if (c == null) continue;

                if (line.Contains("category ="))
                {
                    var m = Regex.Match(line, @"category\s*=\s*""([^""]+)""");
                    if (m.Success)
                    {
                        c.CategoryName = m.Groups[1].Value;
                        if (!instance.categoryTypes.Contains(c.CategoryName))
                        {
                            instance.categoryTypes.Add(c.CategoryName);
                            Msg.Log($":: Load category[{c.Order}] = {c.CategoryName}.");
                        }
                    }
                }

                if (line.Contains("categoryIco ="))
                {
                    var m = Regex.Match(line, @"categoryIco\s*=\s*(\d)");
                    if (m.Success)
                    {
                        c.CategoryIcoId = GetInt(line, "categoryIco", 0);
                        var ico = ItemNameToId.FirstOrDefault(i => i.Value == c.CategoryIcoId);
                        if (ico.Key != null)
                        {
                            c.CategoryIcoName = ico.Key;
                        }
                    }
                }

                var offStart = Regex.IsMatch(line, @"offers\s*=\s*\{");
                if (offStart) continue;

                var offItem = Regex.Match(line, @"\{\s*(.*)\s*\},?");
                if (offItem.Success)
                {
                    var item = new Offers
                    {
                        Names = GetString(line, "names"),
                        Count = GetInt(line, "count", 1),
                        Price = GetInt(line, "price", 0),
                        Desc = GetString(line, "desc"),
                        LookType = GetInt(line, "lookType"),
                        Sex = GetIntOrNull(line, "sex"),
                        Title = GetString(line, "title")
                    };

                    if (item.LookType <= 0)
                    {
                        var found = ItemNameToId.FirstOrDefault(i => i.Key.Equals(item.Names, StringComparison.OrdinalIgnoreCase));
                        if (found.Key != null)
                        {
                            item.ItemId = found.Value;
                        }
                    }
                    c.Offers.Add(item);
                }
            }

            if (c != null)
            {
                Categories.Add(c);
                foreach (var item in Categories)
                {
                    foreach (var item1 in item.Offers)
                    {
                        var offers = (item1.LookType > 0)
                            ? ($"{item1.Title} -> LookType: {item1.LookType}")
                            : ($"{item1.Names} -> ItemID: {item1.ItemId}");
                        Msg.Log($":: {offers}");
                    }
                }
            }
        }

        private string GetString(string line, string key)
        {
            var m = Regex.Match(line, key + @"\s*=\s*""([^""]*)""", RegexOptions.IgnoreCase);
            return m.Success ? m.Groups[1].Value : "";
        }

        private int GetInt(string line, string key, int def = 0)
        {
            var m = Regex.Match(line, key + @"\s*=\s*(\d+)");
            return m.Success ? int.Parse(m.Groups[1].Value) : def;
        }

        private int? GetIntOrNull(string line, string key)
        {
            var m = Regex.Match(line, key + @"\s*=\s*(\d+)");
            return m.Success ? int.Parse(m.Groups[1].Value) : (int?)null;
        }

        private string Escape(string s) => s?.Replace("\"", "\\\"") ?? "";

        public void LoadShopServer(string path)
        {
            if (!File.Exists(path)) path = Config.Current.ServerFilePath;
            if (!File.Exists(path)) throw new FileNotFoundException("Config auto load file: shop_items.lua not found!", path);

            Categories.Clear();
            var text = File.ReadAllText(path);
            ParseServerLua(text);
        }

        public bool Save(string serverPath = "", string clientPath = "")
        {
            serverPath = (serverPath != string.Empty) ? serverPath : Config.Current.ServerFilePath;
            clientPath = (clientPath != string.Empty) ? clientPath : Config.Current.ClientFilePath;

            SaveShopServer(serverPath);
            SaveShopClient(clientPath);
            return true;
        }

        private bool SaveShopServer(string path)
        {
            var lines = new List<string> { "return {" };

            foreach (var c in Categories)
            {
                lines.Add($"    [{c.Order}] = {{");
                lines.Add($"        category = \"{c.CategoryName}\",");

                lines.Add($"        categoryIco = {c.CategoryIcoId},");

                lines.Add("        offers = {");

                foreach (var o in c.Offers)
                {
                    if (o.LookType > 0)
                    {
                        var sex = o.Sex.HasValue ? $", sex = {o.Sex.Value}" : "";
                        lines.Add($"            {{ lookType = {o.LookType}{sex}, price = {o.Price}, title = \"{Escape(o.Title)}\" }},");
                    }
                    else
                    {
                        lines.Add($"            {{ itemid = {o.ItemId}, names = \"{Escape(o.Names)}\", count = {o.Count}, price = {o.Price}, desc = \"{Escape(o.Desc)}\" }},");
                    }
                }

                lines.Add("        },");
                lines.Add("    },");
            }

            lines.Add("}");
            var p = path + ".lua";
            File.WriteAllLines(p, lines);
            Msg.Log($":: Server file saved: {p}");
            return true;
        }

        private bool SaveShopClient(string path)
        {
            var lines = new List<string> { "return {" };

            foreach (var c in Categories)
            {
                lines.Add($"    [{c.Order}] = {{");
                lines.Add($"        category = \"{c.CategoryName}\",");

                lines.Add($"        categoryIco = {c.CategoryIcoId},");

                lines.Add("        offers = {");

                foreach (var o in c.Offers)
                {
                    if (o.LookType > 0)
                    {
                        var sex = o.Sex.HasValue ? $", sex = {o.Sex.Value}" : "";
                        lines.Add($"            {{ lookType = {o.LookType}{sex}, price = {o.Price}, title = \"{Escape(o.Title)}\" }},");
                    }
                    else
                    {
                        lines.Add($"            {{ itemid = {o.ItemId}, names= \"{Escape(o.Names)}\", count = {o.Count}, price = {o.Price}, desc = \"{Escape(o.Desc)}\" }},");
                    }
                }

                lines.Add("        },");
                lines.Add("    },");
            }

            lines.Add("}");
            var p = path + "_client.lua";
            File.WriteAllLines(p, lines);
            Msg.Log($":: Client file saved: {p}");
            return true;
        }
    }
}