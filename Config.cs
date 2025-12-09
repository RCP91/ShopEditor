using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace ShopEditor
{
    internal class Config : F_Main
    {
        private static readonly string ConfigFile = Path.Combine(Application.StartupPath, "config.json");

        [JsonObject(MemberSerialization.OptIn)]
        public class AppSettings
        {
            [JsonProperty] public string ServerFilePath { get; set; } = "";
            [JsonProperty] public string ClientFilePath { get; set; } = "";
            [JsonProperty] public string PathItemServer { get; set; } = "";
            [JsonProperty] public bool AutoSave { get; set; } = false;
            [JsonProperty] public bool AutoLoadLastFile { get; set; } = false;

            [JsonProperty] public List<string> CategoryList { get; set; }
        }
        public static AppSettings Current { get; private set; } = new AppSettings();
        public static void Load()
        {
            try
            {
                if (File.Exists(ConfigFile))
                {
                    string json = File.ReadAllText(ConfigFile);
                    var loaded = JsonConvert.DeserializeObject<AppSettings>(json);
                    if (loaded != null)
                        Current = loaded;
                }
                else
                {
                    Save();
                }
            }
            catch (Exception ex)
            {
                Msg.Error($"Error to load json: {ex.Message}", "Error!");
                Current = new AppSettings();
            }
         
        }
        public static void Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(Current, Formatting.Indented);
                File.WriteAllText(ConfigFile, json);
            }
            catch (Exception ex)
            {
               Msg.Error($"Error to save json: {ex.Message}", "Error!");
            }
        }
        public static void Update(Action<AppSettings> action)
        {
            action(Current);
            Save();
        }
    }
}