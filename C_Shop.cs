using ShopEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ShopEditorPro
{
    public class ShopItem
    {
        public string Name { get; set; } = "";
        public int Count { get; set; } = 1;
        public int Price { get; set; } = 0;
        public string Desc { get; set; } = "";
        public int ItemId { get; set; } = 0; // items.srv
    }
    public class ShopOutfit
    {
        public int LookType { get; set; }
        public int? Sex { get; set; }
        public int Price { get; set; }
        public string Title { get; set; }
    }
    public class ShopCategory
    {
        public string Name { get; set; } = "";
        public string IconItemName { get; set; } = "";
        public int IconItemId { get; set; } = 0;
        public int? LookIco { get; set; } = null;
        public List<ShopItem> Items { get; set; } = new List<ShopItem>();
        public List<ShopOutfit> Outfits { get; set; } = new List<ShopOutfit>();
        public bool IsOutfit => LookIco.HasValue || Outfits.Count > 0;
    }
    public class ShopEditorPro
    {
        public List<ShopCategory> Categories { get; } = new List<ShopCategory>();
        public Dictionary<string, int> ItemNameToId = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        public void LoadItemIds(string itemsSrvPath)
        {
            if (!File.Exists(itemsSrvPath)) return;

            ItemNameToId.Clear();
            var lines = File.ReadAllLines(itemsSrvPath);
            string currentName = "";
            int currentId = 0;

            foreach (var line in lines)
            {
                var l = line.Trim();

                var idMatch = Regex.Match(l, @"TypeID\s*=\s*(\d+)");
                if (idMatch.Success)
                    currentId = int.Parse(idMatch.Groups[1].Value);

                var nameMatch = Regex.Match(l, @"Name\s*=\s*""([^""]+)""");
                if (nameMatch.Success)
                {
                    currentName = nameMatch.Groups[1].Value.Trim();
                    if (currentId > 0 && !string.IsNullOrEmpty(currentName))
                    {
                        // Remove "a " ou "an "
                        var cleanName = currentName;
                        if (cleanName.StartsWith("a ", StringComparison.OrdinalIgnoreCase))
                            cleanName = cleanName.Substring(2);
                        if (cleanName.StartsWith("an ", StringComparison.OrdinalIgnoreCase))
                            cleanName = cleanName.Substring(3);

                        ItemNameToId[cleanName.Trim()] = currentId;
                        ItemNameToId[currentName] = currentId;
          
                    }
                }
            }
        }
        public void LoadServer(string path)
        {
            if (!File.Exists(path)) path = Config.Current.ServerFilePath;
            if (!File.Exists(path)) throw new FileNotFoundException("shop_items.lua não encontrado!", path);

            Categories.Clear();
            var text = File.ReadAllText(path);
            ParseServerLua(text);
            ResolveItemId();
        }
        public void ResolveItemId()
        {
            foreach (var cat in Categories.Where(c => !c.IsOutfit))
            {
                foreach (var item in cat.Items)
                {
                    if (ItemNameToId.TryGetValue(item.Name.Trim(), out int id))
                        item.ItemId = id;
                    else
                        item.ItemId = 0;
                }

                if (!string.IsNullOrEmpty(cat.IconItemName) &&
                    ItemNameToId.TryGetValue(cat.IconItemName.Trim(), out int iconId))
                {
                    cat.IconItemId = iconId;
                }
                else if (cat.Items.Count > 0)
                {
                    cat.IconItemId = cat.Items[0].ItemId;
                }
            }
        }
        public bool Save(string serverPath = null, string clientPath = null)
        {
            serverPath = Config.Current.ServerFilePath;
            clientPath = Config.Current.ClientFilePath;

            ResolveItemId();
            SaveServer(serverPath);
            SaveClient(clientPath);
            return true;
        }
        private bool SaveServer(string path)
        {
            var lines = new List<string> { "return {" };
            foreach (var cat in Categories)
            {
                lines.Add($"    [\"{cat.Name}\"] = {{");
                if (cat.IsOutfit)
                {
                    if (cat.LookIco.HasValue) lines.Add($"        lookIco = {cat.LookIco.Value},");
                    lines.Add("        outfits = {");
                    foreach (var o in cat.Outfits)
                    {
                        var sex = o.Sex.HasValue ? $", sex = {o.Sex.Value}" : "";
                        lines.Add($"            {{ lookType = {o.LookType}{sex}, price = {o.Price}, title = \"{Escape(o.Title)}\" }},");
                    }
                    lines.Add("        },");
                }
                else
                {
                    if (!string.IsNullOrEmpty(cat.IconItemName))
                        lines.Add($"        itemIco = \"{cat.IconItemName}\",");
                    lines.Add("        items = {");
                    foreach (var item in cat.Items)
                    {
                        lines.Add($"            {{ item = \"{item.Name}\", count = {item.Count}, price = {item.Price}, desc = \"{Escape(item.Desc)}\" }},");
                    }
                    lines.Add("        },");
                }
                lines.Add("    },");
            }
            lines.Add("}");
            File.WriteAllLines(path, lines);
            return true;
        }
        private bool SaveClient(string path)
        {
            var lines = new List<string> { "return {" };
            foreach (var cat in Categories)
            {
                lines.Add($"  [\"{cat.Name}\"] = {{");
                if (cat.IsOutfit)
                {
                    if (cat.LookIco.HasValue)
                        lines.Add($"    [\"lookIco\"] = {cat.LookIco.Value},");
                    lines.Add("    [\"outfits\"] = {");
                    foreach (var o in cat.Outfits)
                    {
                        var sexLine = o.Sex.HasValue ? $"[\"sex\"] = {o.Sex.Value},\n        " : "";
                        lines.Add($"      [{o.LookType}] = {{ {sexLine}[\"title\"] = \"{Escape(o.Title)}\", [\"price\"] = {o.Price} }},");
                    }
                    lines.Add("    },");
                }
                else
                {
                    lines.Add($"    [\"itemIco\"] = {cat.IconItemId},");
                    lines.Add("    [\"items\"] = {");
                    int index = 1;
                    foreach (var item in cat.Items)
                    {
                        lines.Add($"      [{index++}] = {{");
                        lines.Add($"        [\"desc\"] = \"{Escape(item.Desc)}\",");
                        lines.Add($"        [\"item\"] = \"{item.Name}\",");
                        lines.Add($"        [\"id\"] = {item.ItemId},");
                        lines.Add($"        [\"count\"] = {item.Count},");
                        lines.Add($"        [\"price\"] = {item.Price},");
                        lines.Add("      },");
                    }
                    lines.Add("    },");
                }
                lines.Add("  },");
            }
            lines.Add("}");
            //File.WriteAllLines(path, lines);
            return true;
        }

        private void ParseServerLua(string lua)
        {
            var lines = lua.Split('\n');
            ShopCategory cat = null;

            foreach (var raw in lines)
            {
                var line = raw.Trim();
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("--")) continue;

                var catMatch = Regex.Match(line, @"^\[\""([^\""]+)\""\]\s*=\s*\{");
                if (catMatch.Success)
                {
                    if (cat != null) Categories.Add(cat);
                    cat = new ShopCategory { Name = catMatch.Groups[1].Value };
                    continue;
                }

                if (cat == null) continue;

                if (line.Contains("itemIco ="))
                {
                    var m = Regex.Match(line, @"itemIco\s*=\s*""([^""]+)""");
                    if (m.Success) cat.IconItemName = m.Groups[1].Value;
                }
                if (line.Contains("lookIco ="))
                {
                    var m = Regex.Match(line, @"lookIco\s*=\s*(\d+)");
                    if (m.Success) cat.LookIco = int.Parse(m.Groups[1].Value);
                }

                if (line.Contains("item = \"") && line.Contains("{"))
                {
                    var item = new ShopItem
                    {
                        Name = GetString(line, "item"),
                        Count = GetInt(line, "count", 1),
                        Price = GetInt(line, "price", 0),
                        Desc = GetString(line, "desc")
                    };
                    cat.Items.Add(item);
                }

                if (line.Contains("lookType ="))
                {
                    var o = new ShopOutfit
                    {
                        LookType = GetInt(line, "lookType"),
                        Sex = GetIntOrNull(line, "sex"),
                        Price = GetInt(line, "price", 0),
                        Title = GetString(line, "title")
                    };
                    cat.Outfits.Add(o);
                }
            }
            if (cat != null) Categories.Add(cat);
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
    }
}