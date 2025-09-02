using Docker.DotNet;
using ViixsDockerManager.DockLight.Shared.Models;
using ViixsDockerManager.Shared.Services;

namespace ViixsDockerManager.DockLight.Services.Interfaces;

public interface IDockerContainersService : IViixsBaseService
{
    Task<IEnumerable<ContainerSummary>> GetContainerListAsync(Guid environmentId, IContainerOperations? containerOperations = null);
}
