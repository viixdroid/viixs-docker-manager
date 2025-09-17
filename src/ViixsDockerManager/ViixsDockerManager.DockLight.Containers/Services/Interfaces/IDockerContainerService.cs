using ViixsDockerManager.DockLight.Models.Details;
using ViixsDockerManager.DockLight.Models.Overview;

namespace ViixsDockerManager.DockLight.Services.Interfaces;

internal interface IDockerContainerService : IDockerService
{
    Task<ContainerDetails> GetContainerDetailAsync(string containerId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ContainerSummary>> GetContainerListAsync(CancellationToken cancellationToken = default);
    Task KillContainerAsync(string containerId, CancellationToken cancellationToken = default);
    Task RestartContainerAsync(string containerId, uint waitBeforeKillInSeconds = 60, CancellationToken cancellationToken = default);
    Task<bool> StartContainerAsync(string containerId, CancellationToken cancellationToken = default);
    Task<bool> StopContainerAsync(string containerId, uint waitBeforeKillInSeconds = 60, CancellationToken cancellationToken = default);
}
