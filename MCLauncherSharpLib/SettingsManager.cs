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

        public static void UpdateLauncherSettings(string key, object val)
        {
            var settings = LoadLauncherSettings();

            if (settings == null)
            {
                settings = new Dictionary<string, JsonValue>();
            }

            if (val == null)
            {
                settings.Remove(key);
            }
            else
            {
                settings[key] = JsonValue.Create(val);
            }

            SaveLauncherSettings(settings);
        }
        public static object? GetLauncherSetting(string key)
        {
            var settings = LoadLauncherSettings();
            if (settings != null && settings.TryGetValue(key, out var value))
            {
                return value;
            }

            return null;
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
