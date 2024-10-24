## Models

### Profile

```csharp
using MCLauncherSharpLib.Models.LauncherProfiles;
```

```csharp
public class LauncherProfiles
{
    [JsonPropertyName("profiles")]
    public Dictionary<string, Profile>? Profiles { get; set; }

    [JsonPropertyName("settings")]
    public Dictionary<string, JsonValue>? Settings { get; set; }

    [JsonPropertyName("version")]
    public int? Version { get; set; }
}

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
```
