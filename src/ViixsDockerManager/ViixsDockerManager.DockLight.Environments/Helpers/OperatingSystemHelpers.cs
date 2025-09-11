using System.Runtime.InteropServices;

namespace ViixsDockerManager.DockLight.Environments.Helpers;

internal static class OperatingSystemHelpers
{
    private static bool IsWindows()
        => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    private static bool IsLinux()
        => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

    private static bool IsMacOS()
        => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

    public static string GetPlatformName()
    {
        const string linux = "Linux";
        const string windows = "Windows";
        const string macOS = "macOS";

        if (IsWindows())
        {
            return windows;
        }
        if (IsLinux())
        {
            return linux;
        }
        if (IsMacOS())
        {
            return macOS;
        }
        return "Unknown";
    }
}
