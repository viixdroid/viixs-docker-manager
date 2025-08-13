using Docker.DotNet.Models;
using DockerManager.Shared.Models.Docker;

namespace DockerManager.Shared.Services.Docker.Interfaces;

public interface IDockerService
{
    Task<IEnumerable<ContainerSummary>> GetContainerListAsync();
}