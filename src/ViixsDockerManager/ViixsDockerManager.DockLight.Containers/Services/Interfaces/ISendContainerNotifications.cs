using ViixsDockerManager.DockLight.Models.Commands;
using ViixsDockerManager.Mediator;
using ViixsDockerManager.Shared.Services;

namespace ViixsDockerManager.DockLight.Services.Interfaces;

internal interface ISendContainerNotifications : IViixsBaseService, ICommandNotificationSender
{
    Task SendContainerKilled(BaseContainerActionCommand containerActionCommand);
    Task SendContainerRestarted(BaseContainerActionCommand containerActionCommand);
    Task SendContainerStarted(BaseContainerActionCommand containerActionCommand, bool isSuccessfullyStarted);
    Task SendContainerStopped(BaseContainerActionCommand containerActionCommand);
}
