using ViixsDockerManager.DockLight.Models.Commands;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.Mediator.Commands;

namespace ViixsDockerManager.DockLight.Handlers;

internal class KillContainerHandler(IDockerServiceFactory dockerServiceFactory, ISendContainerNotifications sendContainerNotifications)
    : DockLightActionHandlerBase<KillContainerCommand>(dockerServiceFactory, sendContainerNotifications)
{
    protected override async Task<bool> ExecuteContainerActionAsync(IDockerContainerService dockerContainerService, KillContainerCommand command, CancellationToken cancellationToken = default)
    {
        await dockerContainerService.KillContainerAsync(command.ContainerId, cancellationToken: cancellationToken);
        return true;
    }
    protected override Task SendContainerNotificationAsync(ISendContainerNotifications sendContainerNotificationsService, KillContainerCommand command, bool containerActionResult)
        => sendContainerNotificationsService.SendContainerKilled(command);
}
