namespace ViixsDockerManager.DockLight.Controllers.Hubs;

public interface IDockLightInformationContext
{
    Task OnContainerStarted(string containerId, string containerName, bool isSuccessfullyStarted);
    Task OnContainerStopped(string containerId, string containerName);
    Task OnContainerRestarted(string containerId, string containerName);
    Task OnContainerKilled(string containerId, string containerName);
}
