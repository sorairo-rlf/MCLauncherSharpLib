# MCLauncherSharpLib

MCLauncherSharpLib is a library for managing Minecraft profiles and settings.

## Install

NuGet�p�b�P�[�W�}�l�[�W�����g�p���ăC���X�g�[���ł��܂��B

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