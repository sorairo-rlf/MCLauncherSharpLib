using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MCLauncherSharpLib.Models.LauncherProfiles
{
    public class Profile
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("lastUsed")]
        public string? LastUsed { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("gameDir")]
        public string? GameDir { get; set; }

        [JsonPropertyName("javaArgs")]
        public string? JavaArgs { get; set; }

        [JsonPropertyName("lastVersionId")]
        public string? LastVersionId { get; set; }

        [JsonPropertyName("created")]
        public string? Created { get; set; }

        [JsonPropertyName("icon")]
        public string? Icon { get; set; }

        [JsonPropertyName("resolution")]
        public Resolution? Resolution { get; set; }
    }
    public class Resolution
    {
        [JsonPropertyName("width")]
        public int? Width { get; set; }

        [JsonPropertyName("height")]
        public int? Height { get; set; }
    }
}
