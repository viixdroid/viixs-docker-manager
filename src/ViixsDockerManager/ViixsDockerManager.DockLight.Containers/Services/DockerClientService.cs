using Docker.DotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ViixsDockerManager.DockLight.Models;
using ViixsDockerManager.DockLight.Services.Interfaces;
using static ViixsDockerManager.DockLight.Constants.DockLightConstants;

namespace ViixsDockerManager.DockLight.Services;

public class DockerClientService(IConfiguration configuration, ILogger<DockerClientService> logger)
    : IDockerClientService
{
    public IDockerClient? GetDockerClient()
    {
        var isRunningInDocker = configuration.GetValue<bool>(DotnetRunningInContainer);
        if (!isRunningInDocker)
        {
            //When not giving any information, the client itself can figure out the protocol.
            return CreateDockerClient();
        }

        var unixProtocol = DockerCommunicationProtocol.UnixCommunication();

        if (!unixProtocol.AddressExists())
        {
            return null;
        }

        var client = CreateDockerClient(unixProtocol.GetProtocolUri());
        logger.LogInformation("Created Docker client with endpoint {Endpoint}", client.Configuration.EndpointBaseUri);
        return client;
    }

    private static DockerClient CreateDockerClient(Uri? dockerUri = null)
    {
        var dockerClientConfig = new DockerClientConfiguration();
        if (dockerUri is not null)
        {
            dockerClientConfig = new DockerClientConfiguration(dockerUri);
        }

        return dockerClientConfig.CreateClient();
    }
}
