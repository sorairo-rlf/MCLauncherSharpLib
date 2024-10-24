using System;

namespace MCLauncherSharpLib
{
    public class LauncherController
    {
        public static void LaunchMinecraft(string profilePath)
        {
            try
            {
                System.Diagnostics.Process.Start("minecraft.exe", $"--workDir \"{profilePath}\"");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Minecraftの起動に失敗しました: {ex.Message}");
            }
        }
    }
}
