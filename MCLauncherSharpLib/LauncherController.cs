using System;
using System.Diagnostics;

namespace MCLauncherSharpLib
{
    public class LauncherController
    {
        private static readonly string[] PossiblePaths = new[]
        {
            @"C:\XboxGames\Minecraft Launcher\Content",
            @"C:\XboxGames\Minecraft Launcher",
            @"C:\Program Files\Minecraft",
            @"C:\Program Files (x86)\Minecraft",
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".minecraft"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Programs", "Minecraft Launcher")
        };
        public static string FindMinecraftExecutable()
        {
            foreach (string path in PossiblePaths)
            {
                string fullPath = Path.Combine(path, "minecraft.exe");
                if (File.Exists(fullPath))
                {
                    Console.WriteLine($"Minecraft found at: {fullPath}");
                    return fullPath;
                }
            }

            Console.WriteLine("Minecraft executable not found.");
            return null;
        }
        public static void LaunchMinecraft()
        {
            string executablePath = FindMinecraftExecutable();
            if (executablePath == null) return;
            try
            {
                Process.Start(executablePath);
                Console.WriteLine($"Successfully launched: {executablePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to launch the executable: {ex.Message}");
            }
        }
    }
}
