using DockerManager.DockerControl.Models;

namespace DockerManager.DockerController.Views.Services.Interfaces;

public interface IContainerService
{
    Task<IEnumerable<ContainerSummary>> GetContainers();
}
