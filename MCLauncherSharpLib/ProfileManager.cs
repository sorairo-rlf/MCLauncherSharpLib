using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

            // JSON形式にシリアライズ
            string profilesJsonText = JsonSerializer.Serialize(launcherProfiles, options);

            WriteLauncherProfiles(ProfilesPath, profilesJsonText);
        }
        private static string ReadLauncherProfiles(string profilePath)
        {

            if (!File.Exists(profilePath))
                throw new FileNotFoundException($"プロファイルファイルが見つかりません: {profilePath}");

            string profilesJsonText = File.ReadAllText(profilePath);
            return profilesJsonText;
        }

        private static void WriteLauncherProfiles(string profilePath, string profilesJsonText)
        {
            if (!File.Exists(profilePath))
                throw new FileNotFoundException($"プロファイルファイルが見つかりません: {profilePath}");
            File.WriteAllText(ProfilesPath, profilesJsonText);
            return;
        }
    }
}
