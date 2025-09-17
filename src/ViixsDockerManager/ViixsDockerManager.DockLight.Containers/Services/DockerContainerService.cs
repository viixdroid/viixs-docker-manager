using Docker.DotNet;
using Docker.DotNet.Models;
using ViixsDockerManager.DockLight.Models.Details;
using ViixsDockerManager.DockLight.Models.Overview;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.DockLight.Shared.Exceptions;

namespace ViixsDockerManager.DockLight.Services;

internal class DockerContainerService(IContainerOperations containerOperations) : IDockerContainerService
{
    public async Task<IEnumerable<ContainerSummary>> GetContainerListAsync(CancellationToken cancellationToken = default) //Todo: add filters
    {
        var containerListParameters = new ContainersListParameters
        {
            All = true
        };

        var containers = await containerOperations.ListContainersAsync(containerListParameters, cancellationToken);
        if (containers.Count == 0)
        {
            throw new NoContainersFoundException();
        }
        return containers.Select(c => (ContainerSummary)c);
    }

    public async Task<ContainerDetails> GetContainerDetailAsync(string containerId, CancellationToken cancellationToken = default)
    {
        var container = await containerOperations.InspectContainerAsync(containerId, cancellationToken);
        return container == null
            ? throw new ContainerNotFoundException(containerId)
            : (ContainerDetails)container;
    }

    public Task<bool> StartContainerAsync(string containerId, CancellationToken cancellationToken = default)
    {
        return containerOperations.StartContainerAsync(containerId, null, cancellationToken);
    }

    public Task<bool> StopContainerAsync(string containerId, uint waitBeforeKillInSeconds = 60, CancellationToken cancellationToken = default)
    {
        var containerStopParameters = new ContainerStopParameters
        {
            WaitBeforeKillSeconds = waitBeforeKillInSeconds
        };

        return containerOperations.StopContainerAsync(containerId, containerStopParameters, cancellationToken);
    }

    public Task RestartContainerAsync(string containerId, uint waitBeforeKillInSeconds = 60, CancellationToken cancellationToken = default)
    {
        var containerRestartParameters = new ContainerRestartParameters
        {
            WaitBeforeKillSeconds = waitBeforeKillInSeconds
        };

        return containerOperations.RestartContainerAsync(containerId, containerRestartParameters, cancellationToken);
    }

    public Task KillContainerAsync(string containerId, CancellationToken cancellationToken = default)
    {
        var containerKillParameters = new ContainerKillParameters();
        return containerOperations.KillContainerAsync(containerId, containerKillParameters, cancellationToken);
    }
}
