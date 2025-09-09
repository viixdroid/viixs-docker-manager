using System.Runtime.InteropServices;
using ViixsDockerManager.DockLight.Environments.Exceptions;
using ViixsDockerManager.DockLight.Shared.Models;

namespace ViixsDockerManager.DockLight.Shared.Factories;

public static class DockerCommunicationProtocolFactory
{
    private static readonly Dictionary<OSPlatform, Func<IDockerCommunicationProtocol>> _factories =
        new Dictionary<OSPlatform, Func<IDockerCommunicationProtocol>>
        {
            { OSPlatform.Linux, DockerCommunicationProtocol.LinuxCommunication },
            { OSPlatform.Windows, DockerCommunicationProtocol.WindowsCommunication }
        };

    public static IDockerCommunicationProtocol GetCommunicationProtocol()
    {
        var factory = _factories.FirstOrDefault(kvp => RuntimeInformation.IsOSPlatform(kvp.Key)).Value;

        if (factory is null)
        {
            throw new RunningInThisOsIsNotSupportedException(RuntimeInformation.OSDescription);
        }

        return factory();
    }
}
