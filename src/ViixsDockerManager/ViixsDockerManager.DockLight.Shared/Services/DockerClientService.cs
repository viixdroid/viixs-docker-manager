using Docker.DotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ViixsDockerManager.DockLight.Shared.Models;
using ViixsDockerManager.DockLight.Shared.Services.Interfaces;
using static ViixsDockerManager.DockLight.Shared.Constants.DockLightConstants;

namespace ViixsDockerManager.DockLight.Shared.Services;

public class DockerClientService(IConfiguration configuration, ILogger<DockerClientService> logger)
    : IDockerClientService
{
    public IDockerClient? GetDockerClient(string? address)
    {
        var isRunningInDocker = configuration.GetValue<bool>(DotnetRunningInContainer);
        if (!isRunningInDocker)
        {
            //When not giving any information, the client itself can figure out the protocol.
            return CreateDockerClient();
        }

        //Check if Ip.Parse is succesfull -> TCP connection
        //Else if it is /var/docker/docker.sock -> unix


        var communicationProtocol = DockerCommunicationProtocol.Create(address);

        if (!communicationProtocol.AddressExists())
        {
            return null;
        }

        var client = CreateDockerClient(communicationProtocol.GetProtocolUri());
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
