using ViixsDockerManager.DockLight.Shared.Models;

namespace ViixsDockerManager.DockLight.Environments.Models.Dtos;

public record DockerProtocol(string Protocol, Uri? ProtocolUri = null)
{
    public static DockerProtocol FromDockerCommunicationProtocol(IDockerCommunicationProtocol dockerCommunicationProtocol)
    {
        return new DockerProtocol(dockerCommunicationProtocol.Protocol, dockerCommunicationProtocol.GetProtocolUri());
    }
}
