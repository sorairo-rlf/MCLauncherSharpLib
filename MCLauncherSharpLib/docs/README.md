# MCLauncherSharpLib

## Overview

MCLauncherSharpLib is a library for managing Minecraft profiles and settings.

## Features

- Manage Minecraft profiles easily
- Licensed under Apache 2.0

## Install

It can be installed using the NuGet package manager.

```bash
dotnet add package MCLauncherSharpLib
```

## Usage

```
using MCLauncherSharpLib;

// Adding LauncherProfile
var launcher_profiles = ProfileManager.LoadLauncherProfiles();

string id = "test_id"

var addProfile = new Profile
{
    Name = "SampleProfile",
    Type = "custom",
    LastVersionId = "1.21.1",
    Icon = "Dirt"
};

launcher_profiles.Profiles.Add(id, addProfile)

ProfileManager.SaveLauncherProfiles(launcher_profiles)

// Launch MinecraftLauncher
LauncherController.LaunchMinecraft();
```

[For detailed instructions, please refer to...](https://github.com/sorairo-rlf/MCLauncherSharpLib/tree/master)

## Models offered

### 1. Profile

#### **Property List**
| PropertyName | Type           | Description                       | Required |
|--------------|--------------|----------------------------|------|
| `Name`         | `string`     | Arbitrary profile name | yes |
| `LastUsed`   | `string?`     | Date of last use | no |
| `Type`      | `string`     | profile type | yes |
| `GameDir`  | `string?`  | Full path to the game directory | no |
| `JavaArgs`         | `string?`     | Arguments for Java | no |
| `LastVersionId`   | `string`     | Version of Minecraft to use | yes |
| `Created`      | `string?`     | Creation date | no |
| `Icon`  | `string?`  | Profile icon in the Minecraft Launcher | yes |Resolution
| `Resolution`  | `Resolution`  | Screen resolution settings for the profile | no |

#### **Usage**

```csharp
using MCLauncherSharpLib.Models

var profile = new Profile
{
    Name = "SampleProfile",
    Type = "custom",
    LastVersionId = "1.21.1",
    Icon = "Dirt"
};
```

### 2. Resolution

#### **Property List**
| PropertyName | Type           | Description                       | Required |
|--------------|--------------|----------------------------|------|
| `Width`         | `int?` | Screen width | no |
| `Height`   | `int?` | Screen height | no |

#### **Usage**

```csharp
using MCLauncherSharpLib.Models

var profile = new Profile
{
    Name = "SampleProfile",
    Type = "custom",
    LastVersionId = "1.20.1",
    Icon = "Dirt",
    Resolution = new Resolution
    {
        Width = 800,
        Height = 600
    }
};
```

### 3. LauncherProfiles

#### **Property List**
| PropertyName | Type           | Description                       | Required |
|--------------|--------------|----------------------------|------|
| `Profiles`         | `Dictionary<string, Profile>` | profile list | yes |
| `Settings`   | `Dictionary<string, JsonValue>` | Profile Settings | yes |
| `Version`   | `int` | Profile version | yes |


## Prerequisites

- .NET 8.0
- Windows environment recommended

## License

This project is licensed under the Apache License 2.0.
