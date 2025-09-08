using System.Runtime.InteropServices;
using ViixsDockerManager.DockLight.Environments.Exceptions;
using ViixsDockerManager.DockLight.Environments.Models.Dtos;

using static ViixsDockerManager.DockLight.Environments.Constants.DockLightEnvironmentConstants.Protocol;

namespace ViixsDockerManager.DockLight.Environments.Factories;

internal static class DockerProtocolFactory
{
    private static readonly Dictionary<OSPlatform, DockerProtocol> _dockerProtocolCache = new Dictionary<OSPlatform, DockerProtocol>()
    {
        { OSPlatform.Linux, new DockerProtocol(new Uri(LinuxDockerEngine)) }, //docker.sock
        { OSPlatform.Windows, new DockerProtocol(new Uri(WindowsDockerEngine)) } //npipe
    };

    public static DockerProtocol GetDockerProtocol()
    {
        var dockerProtocol = _dockerProtocolCache.FirstOrDefault(os => RuntimeInformation.IsOSPlatform(os.Key)).Value;

        if (dockerProtocol is null)
        {
            throw new RunningInThisOsIsNotSupportedException(RuntimeInformation.OSDescription);
        }

        return dockerProtocol;
    }


}
