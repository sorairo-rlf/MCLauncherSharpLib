using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCLauncherSharpLib.Models.MinecraftVersions
{
    public class VersionManifest
    {
        public Latest? Latest { get; set; }
        public List<VersionInfo>? Versions { get; set; }
    }
    public class Latest
    {
        public string? Release { get; set; }
        public string? Snapshot { get; set; }
    }
}
