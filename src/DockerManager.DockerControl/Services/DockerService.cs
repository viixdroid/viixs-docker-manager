using Docker.DotNet;
using Docker.DotNet.Models;
using DockerManager.DockerControl.Models;
using DockerManager.DockerControl.Services.Interfaces;

namespace DockerManager.DockerControl.Services;

internal class DockerService(IDockerClientService dockerClientService) : IDockerService
{
    private readonly IDockerClient _dockerClient = dockerClientService.GetDockerClient() ??
                                                   throw new InvalidOperationException(
                                                       "Could not find a correct communciation protocol for docker");

    public async Task<IEnumerable<ContainerSummary>> GetContainerListAsync()
    {
        //TODO: Actually add some filters.
        var containerListParameters = new ContainersListParameters()
        {
            All = true
        };
        var containers = await _dockerClient.Containers.ListContainersAsync(containerListParameters);
        return containers.Select(c => (ContainerSummary)c);
    }
}