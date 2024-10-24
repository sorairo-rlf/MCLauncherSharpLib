using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace MCLauncherSharpLib.Models.LauncherProfiles
{
    public class LauncherProfiles
    {
        [JsonPropertyName("profiles")]
        public Dictionary<string, Profile>? Profiles { get; set; }

        [JsonPropertyName("settings")]
        public Dictionary<string, JsonValue>? Settings { get; set; }

        [JsonPropertyName("version")]
        public int? Version { get; set; }
    }
}
