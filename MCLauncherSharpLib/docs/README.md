# MCLauncherSharpLib

MCLauncherSharpLib is a library for managing Minecraft profiles and settings.

## Install

It can be installed using the NuGet package manager.

```bash
dotnet add package MCLauncherSharpLib
```

## Examples of Use

```csharp
var profile = new Profile { Name = "MyProfile", Version = "1.19" };
UpsertProfile("myProfileId", profile);
```

## License

This project is licensed under the Apache License 2.0.