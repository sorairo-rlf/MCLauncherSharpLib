using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

using MCLauncherSharpLib.Models.LauncherProfiles;

namespace MCLauncherSharpLib
{
    public class ProfileManager
    {
        private static readonly string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string MinecraftDataPath = Path.Combine(AppDataPath, ".minecraft");
        private static readonly string ProfilesPath = Path.Combine(MinecraftDataPath, "launcher_profiles.json");

        public static LauncherProfiles LoadLauncherProfiles()
        {
            string profilesJsonText = ReadLauncherProfiles(ProfilesPath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<LauncherProfiles>(profilesJsonText, options)
                   ?? new LauncherProfiles();
        }

        public static void SaveLauncherProfiles(LauncherProfiles launcherProfiles)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            string profilesJsonText = JsonSerializer.Serialize(launcherProfiles, options);

            WriteLauncherProfiles(ProfilesPath, profilesJsonText);
        }

        public static Dictionary<string, Profile> LoadProfiles()
        {
            var profiles = LoadLauncherProfiles();
            if (profiles.Profiles == null)
            {
                profiles.Profiles = new Dictionary<string, Profile>();
            }
            return profiles.Profiles;
        }

        public static void UpsertProfile(string id, Profile profile)
        {
            var profiles = LoadLauncherProfiles();
            if (profiles.Profiles == null)
            {
                profiles.Profiles = new Dictionary<string, Profile>();
            }

            profiles.Profiles[id] = profile;
            SaveLauncherProfiles(profiles);
        }

        public static void AddProfile(string id, Profile addProfile)
        {
            var profiles = LoadLauncherProfiles();
            if (profiles.Profiles != null)
                profiles.Profiles.Add(id, addProfile);
            SaveLauncherProfiles(profiles);
        }
        public static void RemoveProfile(string id)
        {
            var profiles = LoadLauncherProfiles();
            if (profiles.Profiles != null && profiles.Profiles.Remove(id))
            {
                SaveLauncherProfiles(profiles);
            }
            else
            {
                throw new KeyNotFoundException($"Profile with key '{id}' does not exist.");
            }
        }
        public static void ReplaceProfile(string id, Profile repProfile)
        {
            var profiles = LoadLauncherProfiles();
            if (profiles.Profiles != null && profiles.Profiles.ContainsKey(id))
            {
                profiles.Profiles[id] = repProfile;
                SaveLauncherProfiles(profiles);
            }
            else
            {
                throw new KeyNotFoundException($"Profile with key '{id}' does not exist.");
            }
        }

        public static void UpdateProfilesSettings(string key, object val)
        {
            var profiles = LoadLauncherProfiles();

            if (profiles.Settings == null)
            {
                profiles.Settings = new Dictionary<string, JsonValue>();
            }

            if (val == null)
            {
                profiles.Settings.Remove(key);
            }
            else
            {
                profiles.Settings[key] = JsonValue.Create(val);
            }

            SaveLauncherProfiles(profiles);
        }
        public static object? GetProfileSetting(string key)
        {
            var profiles = LoadLauncherProfiles();
            if (profiles.Settings != null && profiles.Settings.TryGetValue(key, out var value))
            {
                return value;
            }

            return null;
        }

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
