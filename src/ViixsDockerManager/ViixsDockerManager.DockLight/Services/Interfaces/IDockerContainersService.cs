using ViixsDockerManager.DockLight.Models;

namespace ViixsDockerManager.DockLight.Services.Interfaces;

public interface IDockerContainersService
{
    Task<IEnumerable<ContainerSummary>> GetContainerListAsync();
}
