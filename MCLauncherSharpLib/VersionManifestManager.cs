using MCLauncherSharpLib.Models.MinecraftVersions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MCLauncherSharpLib
{
    public class VersionManifestManager
    {
        private static readonly string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string MinecraftDataPath = Path.Combine(AppDataPath, ".minecraft");
        private static readonly string VersionManifestPath = Path.Combine(MinecraftDataPath, "versions", "version_manifest_v2.json");

        public static VersionManifest LoadVersionManifest()
        {
            string versionManifestJsonText = ReadVersionManifest(VersionManifestPath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<VersionManifest>(versionManifestJsonText, options)
                   ?? new VersionManifest();
        }

        public static void SaveVersionManifest(VersionManifest versionManifest)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            string versionManifestJsonText = JsonSerializer.Serialize(versionManifest, options);

            WriteVersionManifest(VersionManifestPath, versionManifestJsonText);
        }

        public static List<string> GetMinecraftVersionList()
        {
            var versionManifest = LoadVersionManifest();

            if (versionManifest == null || versionManifest.Versions == null)
            {
                return new List<string>();
            }

            List<string> versionList = versionManifest.Versions.Select(version => version.Id).ToList();

            return versionList;
        }

        public static bool IsVersionExists(string versionId)
        {
            var versions = GetMinecraftVersionList();
            return versions.Contains(versionId);
        }

        public static string GetLatestVersion()
        {
            var versionManifest = LoadVersionManifest();
            return versionManifest?.Latest?.Release ?? "No release version found.";
        }
        public static string GetLatestSnapshot()
        {
            var versionManifest = LoadVersionManifest();
            return versionManifest?.Latest?.Snapshot ?? "No snapshot version found.";
        }

        private static string ReadVersionManifest(string versionManifestPath)
        {

            if (!File.Exists(versionManifestPath))
                throw new FileNotFoundException($"versionManifest file not found: {versionManifestPath}");

            string versionManifestJsonText = File.ReadAllText(versionManifestPath);
            return versionManifestJsonText;
        }

        private static void WriteVersionManifest(string versionManifestPath, string versionManifestJsonText)
        {
            if (!File.Exists(versionManifestPath))
                throw new FileNotFoundException($"versionManifest file not found: {versionManifestPath}");
            File.WriteAllText(versionManifestPath, versionManifestJsonText);
            return;
        }
    }
}
