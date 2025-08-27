using Docker.DotNet;
using Docker.DotNet.Models;
using ViixsDockerManager.DockLight.Exceptions;
using ViixsDockerManager.DockLight.Models;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.Shared.Exceptions;

namespace ViixsDockerManager.DockLight.Services;

public class DockerContainersService : IDockerContainersService
{
    private readonly IContainerOperations _containerOperations;

    public DockerContainersService(IDockerClientService dockerClientService)
    {
        var dockerClient = dockerClientService.GetDockerClient() ??
                           throw ViixsDockerManagerException.InvalidOperation(
                               "Could not find a correct communication protocol for docker");
        _containerOperations = dockerClient.Containers;
    }

    public async Task<IEnumerable<ContainerSummary>> GetContainerListAsync()
    {
        //TODO: Actually add some filters.
        var containerListParameters = new ContainersListParameters
        {
            All = true
        };
        var containers = await _containerOperations.ListContainersAsync(containerListParameters);
        if (containers.Count == 0)
        {
            throw new NoContainersFoundException();
        }
        return containers.Select(c => (ContainerSummary)c);
    }
}
