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
            string profilesJsonText = ReadLauncherSettings(SettingsPath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<Dictionary<string, JsonValue>>(profilesJsonText, options)
                   ?? new Dictionary<string, JsonValue>();
        }
        public static void SaveLauncherProfiles(Dictionary<string, JsonValue> launcherSettings)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            // JSON形式にシリアライズ
            string settingsJsonText = JsonSerializer.Serialize(launcherSettings, options);

            WriteLauncherSettings(SettingsPath, settingsJsonText);
        }

        public static void UpdateProfilesSettings(string key, object val)
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

            SaveLauncherProfiles(settings);
        }
        public static object? GetProfileSetting(string key)
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
                throw new FileNotFoundException($"プロファイルファイルが見つかりません: {settingsPath}");

            string profilesJsonText = File.ReadAllText(settingsPath);
            return profilesJsonText;
        }

        private static void WriteLauncherSettings(string settingsPath, string settingsJsonText)
        {
            if (!File.Exists(settingsPath))
                throw new FileNotFoundException($"プロファイルファイルが見つかりません: {settingsPath}");
            File.WriteAllText(settingsPath, settingsJsonText);
            return;
        }
    }
}
