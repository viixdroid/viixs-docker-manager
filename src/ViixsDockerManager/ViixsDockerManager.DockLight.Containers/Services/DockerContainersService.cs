using Docker.DotNet;
using Docker.DotNet.Models;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.DockLight.Shared.Exceptions;
using ViixsDockerManager.DockLight.Shared.Models;
using ViixsDockerManager.DockLight.Shared.Queries.Filters;
using ViixsDockerManager.DockLight.Shared.Services.Interfaces;
using ViixsDockerManager.Shared.Database.Repositories;
using ViixsDockerManager.Shared.Exceptions;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.DockLight.Services;

public class DockerContainersService : IDockerContainersService
{
    private readonly IReadRepository<DockLightEnvironment> _dockLightEnvironmentRepository;
    // private readonly IContainerOperations _containerOperations;

    public DockerContainersService(IDockerClientService dockerClientService,
        IReadRepository<DockLightEnvironment> dockLightEnvironmentRepository)
    {
        _dockLightEnvironmentRepository = dockLightEnvironmentRepository;
        // var dockerClient = dockerClientService.GetDockerClient() ??
        //                    throw ViixsDockerManagerException.InvalidOperation(
        //                        "Could not find a correct communication protocol for docker");
        // _containerOperations = dockerClient.Containers;
    }

    public async Task<IEnumerable<ContainerSummary>> GetContainerListAsync(Guid environmentId, IContainerOperations? containerOperations = null)
    {
        containerOperations = Guard.ValueIsNotNull(containerOperations, nameof(containerOperations));

        // var environment = await _dockLightEnvironmentRepository.GetByFilterAsync(new DockLightEnvironmentByEnvironmentIdFilter(environmentId));
        //
        // if (environment is null)
        // {
        //     throw new InvalidOperationException("No environments found.");
        // }

        //TODO: Actually add some filters.
        var containerListParameters = new ContainersListParameters { All = true };
        var containers = await containerOperations.ListContainersAsync(containerListParameters);
        if (containers.Count == 0)
        {
            throw new NoContainersFoundException();
        }

        return containers.Select(c => (ContainerSummary)c);
    }
}
