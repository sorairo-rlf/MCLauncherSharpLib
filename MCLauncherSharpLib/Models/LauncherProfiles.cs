using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace MCLauncherSharpLib.Models
{
    public class LauncherProfiles
    {
        [JsonPropertyName("profiles")]
        public required Dictionary<string, Profile> Profiles { get; set; }

        [JsonPropertyName("settings")]
        public required Dictionary<string, JsonValue> Settings { get; set; }

        [JsonPropertyName("version")]
        public required int Version { get; set; }
    }
}
