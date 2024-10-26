using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MCLauncherSharpLib
{
    public class SettingsManager
    {
        private static readonly string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string MinecraftDataPath = Path.Combine(AppDataPath, ".minecraft");
        private static readonly string SettingsPath = Path.Combine(MinecraftDataPath, "launcher_settings.json");
        public static Dictionary<string, JsonValue> LoadLauncherSettings()
        {
            string settingsJsonText = ReadLauncherSettings(SettingsPath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<Dictionary<string, JsonValue>>(settingsJsonText, options)
                   ?? new Dictionary<string, JsonValue>();
        }
        public static void SaveLauncherSettings(Dictionary<string, JsonValue> launcherSettings)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            string settingsJsonText = JsonSerializer.Serialize(launcherSettings, options);

            WriteLauncherSettings(SettingsPath, settingsJsonText);
        }

        public static JsonValue? GetSetting(string key)
        {
            var settings = LoadLauncherSettings();
            if (settings != null)
            {
                if (settings.ContainsKey(key))
                {
                    JsonValue getSetting = settings[key];
                    return getSetting;
                }
                Console.WriteLine($"Error: No launcher setting found with the Key '{key}'.");
                return null;
            }
            Console.WriteLine("Error: Launcher settings are null.");
            return null;
        }
        public static bool AddSetting(string key, JsonValue addSetting)
        {
            var settings = LoadLauncherSettings();
            if (settings == null)
                settings = new Dictionary<string, JsonValue>();
            if (settings.ContainsKey(key))
            {
                Console.WriteLine($"Error: No launcher setting found with the Key '{key}'.");
                return false;
            }
            settings.Add(key, addSetting);
            SaveLauncherSettings(settings);
            return true;
        }
        public static bool UpsertSetting(string key, JsonValue upsSetting)
        {
            var settings = LoadLauncherSettings();
            if (settings == null)
                settings = new Dictionary<string, JsonValue>();
            settings[key] = upsSetting;
            SaveLauncherSettings(settings);
            return true;
        }
        public static bool ReplaceSetting(string key, JsonValue repSetting)
        {
            var settings = LoadLauncherSettings();
            if (settings != null)
            {
                if (settings.ContainsKey(key))
                {
                    settings[key] = repSetting;
                    SaveLauncherSettings(settings);
                }
                Console.WriteLine($"Error: No launcher setting found with the Key '{key}'.");
                return false;
            }
            Console.WriteLine("Error: Launcher settings are null.");
            return false;
        }
        public static bool DeleteSetting(string key)
        {
            var settings = LoadLauncherSettings();
            if (settings != null)
            {
                if (settings.ContainsKey(key))
                {
                    settings.Remove(key);
                    SaveLauncherSettings(settings);
                }
                return true;
            }
            Console.WriteLine("Error: Launcher settings are null.");
            return false;
        }
        public static bool RemoveSetting(string key)
        {
            var settings = LoadLauncherSettings();
            if (settings != null)
            {
                if (settings.ContainsKey(key))
                {
                    settings.Remove(key);
                    SaveLauncherSettings(settings);
                    return true;
                }
                Console.WriteLine($"Error: No launcher setting found with the Key '{key}'.");
                return false;
            }
            Console.WriteLine("Error: Launcher settings are null.");
            return false;
        }

        private static string ReadLauncherSettings(string settingsPath)
        {

            if (!File.Exists(settingsPath))
                throw new FileNotFoundException($"LauncherSettings file not found: {settingsPath}");

            string settingsJsonText = File.ReadAllText(settingsPath);
            return settingsJsonText;
        }
        private static void WriteLauncherSettings(string settingsPath, string settingsJsonText)
        {
            if (!File.Exists(settingsPath))
                throw new FileNotFoundException($"LauncherSettings file not found: {settingsPath}");
            File.WriteAllText(settingsPath, settingsJsonText);
            return;
        }
    }
}
