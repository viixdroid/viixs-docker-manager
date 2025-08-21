using DockerManager.DockerControl.Models;

namespace DockerManager.DockerControl.Services.Interfaces;

public interface IDockerService
{
    Task<IEnumerable<ContainerSummary>> GetContainerListAsync();
}