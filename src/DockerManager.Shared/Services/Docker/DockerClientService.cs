using Docker.DotNet;
using DockerManager.Shared.Models.Docker;
using DockerManager.Shared.Services.Docker.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DockerManager.Shared.Services.Docker;

public class DockerClientService(IConfiguration configuration) : IDockerClientService
{
    public IDockerClient? GetDockerClient()
    {
        var isRunningInDocker = configuration.GetValue<bool>("DOTNET_RUNNING_IN_CONTAINER");
        if (!isRunningInDocker)
        {
            //When not giving any information, the client itself can figure out the protocol.
            return CreateDockerClient();
        }

        var unixProtocol = DockerCommunicationProtocol.UnixCommunication();

        if (unixProtocol.AddressExists())
        {
            return CreateDockerClient(unixProtocol.GetProtocolUri());
        }

        var windowsProtocol = DockerCommunicationProtocol.WindowsCommunication();

        if (windowsProtocol.AddressExists())
        {
            return CreateDockerClient(windowsProtocol.GetProtocolUri());
        }

        return null;
    }

    //TODO: Add logging.
    private static IDockerClient CreateDockerClient(Uri? dockerUri = null)
    {
        var dockerClientConfig = new DockerClientConfiguration();
        if (dockerUri is not null)
        {
            dockerClientConfig = new DockerClientConfiguration(dockerUri);
        }

        return dockerClientConfig.CreateClient();
    }
}