using Docker.DotNet;
using Microsoft.Extensions.Logging;
using ViixsDockerManager.DockLight.Shared.Services.Interfaces;

namespace ViixsDockerManager.DockLight.Shared.Services;

public class DockerClientService(ILogger<DockerClientService> logger)
    : IDockerClientService
{
    public IDockerClient? GetDockerClient(string? address)
    {
        ArgumentException.ThrowIfNullOrEmpty(address);

        var client = CreateDockerClient(new Uri(address));
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
