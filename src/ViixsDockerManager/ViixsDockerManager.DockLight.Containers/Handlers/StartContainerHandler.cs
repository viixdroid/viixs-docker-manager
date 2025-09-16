using ViixsDockerManager.DockLight.Models.Commands;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Shared.Exceptions;

namespace ViixsDockerManager.DockLight.Handlers;

internal class StartContainerHandler(IDockerServiceFactory dockerServiceFactory, ISendContainerNotifications sendContainerNotifications)
    : DockLightActionHandlerBase<StartContainerCommand>(dockerServiceFactory, sendContainerNotifications)
{
    protected override Task<bool> ExecuteContainerActionAsync(IDockerContainerService dockerContainerService, StartContainerCommand command, CancellationToken cancellationToken = default)
    {
        return dockerContainerService.StartContainerAsync(command.ContainerId, cancellationToken: cancellationToken);
    }
    protected override Task SendContainerNotificationAsync(ISendContainerNotifications sendContainerNotificationsService, StartContainerCommand command, bool containerActionResult)
        => sendContainerNotificationsService.SendContainerStarted(command, containerActionResult);
}
