using ViixsDockerManager.DockLight.Models.Details;
using ViixsDockerManager.DockLight.Models.Overview;

namespace ViixsDockerManager.DockLight.Services.Interfaces;

internal interface IDockerContainerService : IDockerService
{
    Task<ContainerDetails> GetContainerDetailAsync(string containerId);
    Task<IEnumerable<ContainerSummary>> GetContainerListAsync();
}
