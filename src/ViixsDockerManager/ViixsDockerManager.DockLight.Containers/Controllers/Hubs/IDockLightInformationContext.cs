namespace ViixsDockerManager.DockLight.Controllers.Hubs;

public interface IDockLightInformationContext
{
    Task OnContainerStarted(string containerId, bool isSuccessfullyStarted);
    Task OnContainerStopped(string containerId);
    Task OnContainerRestarted(string containerId);
    Task OnContainerKilled(string containerId);
}
