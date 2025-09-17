using ViixsDockerManager.DockLight.Models.Commands;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.Mediator.Commands;

namespace ViixsDockerManager.DockLight.Handlers;

internal class RestartContainerHandler(IDockerServiceFactory dockerServiceFactory, ISendContainerNotifications sendContainerNotifications)
    : DockLightActionHandlerBase<RestartContainerCommand>(dockerServiceFactory, sendContainerNotifications)
{
    protected override async Task<bool> ExecuteContainerActionAsync(IDockerContainerService dockerContainerService, RestartContainerCommand command, CancellationToken cancellationToken = default)
    {
        await dockerContainerService.RestartContainerAsync(command.ContainerId, cancellationToken: cancellationToken);
        return true;
    }
    protected override Task SendContainerNotificationAsync(ISendContainerNotifications sendContainerNotificationsService, RestartContainerCommand command, bool containerActionResult)
        => sendContainerNotificationsService.SendContainerRestarted(command);
}
