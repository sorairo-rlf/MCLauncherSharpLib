using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace MCLauncherSharpLib.Models.LauncherProfiles
{
    public class Settings
    {
        [JsonPropertyName("locale")]
        public string? Locale { get; set; }

        [JsonPropertyName("showMenu")]
        public bool? ShowMenu { get; set; }

        // 未知の設定項目を保持するためのディクショナリ
        [JsonExtensionData]
        public Dictionary<string, JsonElement>? AdditionalSettings { get; set; }
    }
}
