using ViixsDockerManager.DockLight.Models.Commands;
using ViixsDockerManager.Shared.Services;

namespace ViixsDockerManager.DockLight.Services.Interfaces;

internal interface ISendContainerNotifications : IViixsBaseService
{
    Task SendContainerKilled(BaseContainerActionCommand containerActionCommand);
    Task SendContainerRestarted(BaseContainerActionCommand containerActionCommand);
    Task SendContainerStarted(BaseContainerActionCommand containerActionCommand, bool isSuccessfullyStarted);
    Task SendContainerStopped(BaseContainerActionCommand containerActionCommand);
}
