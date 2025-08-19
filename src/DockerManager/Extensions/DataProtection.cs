using System.Runtime.InteropServices;
using Microsoft.AspNetCore.DataProtection;

using static DockerManager.Shared.Constants.ApplicationConstants;
using static DockerManager.Shared.Constants.ApplicationConstants.DataProtection;

namespace DockerManager.Extensions;

internal static class DataProtection
{
    // public const string Get
    public static IServiceCollection AddDataProtectionServices(this IServiceCollection services)
    {
        var keysFolder = DockerKeysFolder;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            //We are debugging on Windows, so we use the local app data folder
            keysFolder = GetWindowsKeysFolder();
        }

        services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(keysFolder))
            .SetApplicationName(ApplicationName);

        return services;
    }
}
