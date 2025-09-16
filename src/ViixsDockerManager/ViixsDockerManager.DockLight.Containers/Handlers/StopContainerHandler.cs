using ViixsDockerManager.DockLight.Models.Commands;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.Mediator.Commands;

namespace ViixsDockerManager.DockLight.Handlers;

internal class StopContainerHandler(IDockerServiceFactory dockerServiceFactory, ISendContainerNotifications sendContainerNotifications)
    : DockLightActionHandlerBase<StopContainerCommand>(dockerServiceFactory, sendContainerNotifications)
{
    protected override async Task<bool> ExecuteContainerActionAsync(IDockerContainerService dockerContainerService, StopContainerCommand command, CancellationToken cancellationToken = default)
    {
        await dockerContainerService.StopContainerAsync(command.ContainerId, cancellationToken: cancellationToken);
        return true;
    }
    protected override Task SendContainerNotificationAsync(ISendContainerNotifications sendContainerNotificationsService, StopContainerCommand command, bool containerActionResult)
        => sendContainerNotificationsService.SendContainerStopped(command);
}
