using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using MCLauncherSharpLib.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MCLauncherSharpLib
{
    public class ProfileManager
    {
        private static readonly string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string MinecraftDataPath = Path.Combine(AppDataPath, ".minecraft");
        private static readonly string ProfilesPath = Path.Combine(MinecraftDataPath, "launcher_profiles.json");

        // common
        public static LauncherProfiles LoadLauncherProfiles()
        {
            string profilesJsonText = ReadLauncherProfiles(ProfilesPath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<LauncherProfiles>(profilesJsonText, options)
                   ?? new LauncherProfiles() {
                        Profiles = new Dictionary<string, Profile> {},
                        Settings = new Dictionary<string, JsonValue> {},
                        Version = -1
                   };
        }
        public static void SaveLauncherProfiles(LauncherProfiles launcherProfiles)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string profilesJsonText = JsonSerializer.Serialize(launcherProfiles, options);
            WriteLauncherProfiles(ProfilesPath, profilesJsonText);
        }

        // Profiles
        public static Dictionary<string, Profile> LoadProfiles()
        {
            var launcher_profiles = LoadLauncherProfiles();
            return launcher_profiles.Profiles
                ?? new Dictionary<string, Profile>();
        }
        public static void SaveProfiles(Dictionary<string, Profile> profiles)
        {
            var launcher_profiles = LoadLauncherProfiles();
            launcher_profiles.Profiles = profiles;
            SaveLauncherProfiles(launcher_profiles);
        }

        public static Profile? GetProfile(string id)
        {
            var profiles = LoadProfiles();
            if (profiles != null)
            {
                if (profiles.ContainsKey(id))
                {
                    var getProfile = profiles[id];
                    return getProfile;
                }
                Console.WriteLine($"Error: No profile found with the ID '{id}'.");
                return null;
            }
            Console.WriteLine("Error: Launcher profiles are null.");
            return null;
        }
        public static bool AddProfile(string id, Profile addProfile)
        {
            var profiles = LoadProfiles();
            if (profiles == null)
                profiles = new Dictionary<string, Profile>();
            if (profiles.ContainsKey(id))
            {
                Console.WriteLine($"Error: A profile with the ID '{id}' already exists.");
                return false;
            }
            profiles.Add(id, addProfile);
            SaveProfiles(profiles);
            return true;
        }
        public static bool UpsertProfile(string id, Profile upsProfile)
        {
            var launcher_profiles = LoadLauncherProfiles();
            if (launcher_profiles.Profiles == null)
                launcher_profiles.Profiles = new Dictionary<string, Profile>();
            launcher_profiles.Profiles[id] = upsProfile;
            SaveLauncherProfiles(launcher_profiles);
            return true;
        }
        public static bool ReplaceProfile(string id, Profile repProfile)
        {
            var launcher_profiles = LoadLauncherProfiles();
            if (launcher_profiles.Profiles != null)
            {
                if (launcher_profiles.Profiles.ContainsKey(id))
                {
                    launcher_profiles.Profiles[id] = repProfile;
                    SaveLauncherProfiles(launcher_profiles);
                }
                Console.WriteLine($"Error: No profile found with the ID '{id}'.");
                return false;
            }
            Console.WriteLine("Error: Launcher profiles are null.");
            return false;
        }
        public static bool DeleteProfile(string id)
        {
            var launcher_profiles = LoadLauncherProfiles();
            if (launcher_profiles.Profiles != null)
            {
                if (launcher_profiles.Profiles.ContainsKey(id))
                {
                    launcher_profiles.Profiles.Remove(id);
                    SaveLauncherProfiles(launcher_profiles);
                }
                return true;
            }
            Console.WriteLine("Error: Launcher profiles are null.");
            return false;
        }
        public static bool RemoveProfile(string id)
        {
            var profiles = LoadProfiles();
            if (profiles != null)
            {
                if (profiles.ContainsKey(id))
                {
                    profiles.Remove(id);
                    SaveProfiles(profiles);
                    return true;
                }
                Console.WriteLine($"Error: No profile found with the ID '{id}'.");
                return false;
            }
            Console.WriteLine("Error: Launcher profiles are null.");
            return false;
        }

        // Settings
        public static Dictionary<string, JsonValue> LoadSettings()
        {
            var launcher_profiles = LoadLauncherProfiles();
            return launcher_profiles.Settings
                ?? new Dictionary<string, JsonValue>();
        }
        public static void SaveSettings(Dictionary<string, JsonValue> settings)
        {
            var launcher_profiles = LoadLauncherProfiles();
            launcher_profiles.Settings = settings;
            SaveLauncherProfiles(launcher_profiles);
        }

        public static JsonValue? GetSetting(string key)
        {
            var settings = LoadSettings();
            if (settings != null)
            {
                if (settings.ContainsKey(key))
                {
                    JsonValue getSetting = settings[key];
                    return getSetting;
                }
                Console.WriteLine($"Error: No setting found with the Key '{key}'.");
                return null;
            }
            Console.WriteLine("Error: LauncherProfiles settings are null.");
            return null;
        }
        public static bool AddSetting(string key, JsonValue addSetting)
        {
            var settings = LoadSettings();
            if (settings == null)
                settings = new Dictionary<string, JsonValue>();
            if (settings.ContainsKey(key))
            {
                Console.WriteLine($"Error: No setting found with the Key '{key}'.");
                return false;
            }
            settings.Add(key, addSetting);
            SaveSettings(settings);
            return true;
        }
        public static bool UpsertSetting(string key, JsonValue upsSetting)
        {
            var launcher_profiles = LoadLauncherProfiles();
            if (launcher_profiles.Settings == null)
                launcher_profiles.Settings = new Dictionary<string, JsonValue>();
            launcher_profiles.Settings[key] = upsSetting;
            SaveLauncherProfiles(launcher_profiles);
            return true;
        }
        public static bool ReplaceSetting(string key, JsonValue repSetting)
        {
            var launcher_profiles = LoadLauncherProfiles();
            if (launcher_profiles.Settings != null)
            {
                if (launcher_profiles.Settings.ContainsKey(key))
                {
                    launcher_profiles.Settings[key] = repSetting;
                    SaveLauncherProfiles(launcher_profiles);
                }
                Console.WriteLine($"Error: No setting found with the Key '{key}'.");
                return false;
            }
            Console.WriteLine("Error: LauncherProfiles settings are null.");
            return false;
        }
        public static bool DeleteSetting(string key)
        {
            var launcher_profiles = LoadLauncherProfiles();
            if (launcher_profiles.Settings != null)
            {
                if (launcher_profiles.Settings.ContainsKey(key))
                {
                    launcher_profiles.Settings.Remove(key);
                    SaveLauncherProfiles(launcher_profiles);
                }
                return true;
            }
            Console.WriteLine("Error: LauncherProfiles settings are null.");
            return false;
        }
        public static bool RemoveSetting(string key)
        {
            var settings = LoadSettings();
            if (settings != null)
            {
                if (settings.ContainsKey(key))
                {
                    settings.Remove(key);
                    SaveSettings(settings);
                    return true;
                }
                Console.WriteLine($"Error: No setting found with the Key '{key}'.");
                return false;
            }
            Console.WriteLine("Error: LauncherProfiles settings are null.");
            return false;
        }

        // Version
        public static int GetVersion()
        {
            var launcher_profiles = LoadLauncherProfiles();
            return launcher_profiles.Version;
        }

        // private
        private static string ReadLauncherProfiles(string profilePath)
        {

            if (!File.Exists(profilePath))
                throw new FileNotFoundException($"Profile file not found: {profilePath}");
            string profilesJsonText = File.ReadAllText(profilePath);
            return profilesJsonText;
        }
        private static void WriteLauncherProfiles(string profilePath, string profilesJsonText)
        {
            if (!File.Exists(profilePath))
                throw new FileNotFoundException($"Profile file not found: {profilePath}");
            File.WriteAllText(profilePath, profilesJsonText);
            return;
        }
    }
}