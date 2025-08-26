using ViixsDockerManager.DockLight.Models;
using ViixsDockerManager.Shared.Services;

namespace ViixsDockerManager.DockLight.Services.Interfaces;

public interface IDockerContainersService : IViixsBaseService
{
    Task<IEnumerable<ContainerSummary>> GetContainerListAsync();
}
