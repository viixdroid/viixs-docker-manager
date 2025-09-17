using Docker.DotNet;
using ViixsDockerManager.DockLight.Models;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Shared.Exceptions;

namespace ViixsDockerManager.DockLight.Handlers;

internal abstract class DockLightActionHandlerBase<TCommand>(IDockerServiceFactory dockerServiceFactory, ISendContainerNotifications sendContainerNotifications)
    : DockLightHandlerBase(dockerServiceFactory), ICommandHandler<TCommand>
    where TCommand : ICommand, IEnvironmentContext
{
    Task ICommandHandler<TCommand>.Handle(TCommand command, CancellationToken cancellationToken) => Handle(command, cancellationToken);

    protected virtual Task SendContainerNotificationAsync(ISendContainerNotifications sendContainerNotificationsService, TCommand command, bool containerActionResult) => Task.CompletedTask;

    protected abstract Task<bool> ExecuteContainerActionAsync(IDockerContainerService dockerContainerService, TCommand command, CancellationToken cancellationToken = default);

    private async Task Handle(TCommand command, CancellationToken cancellationToken)
    {
        var service = await GetDockerContainerService(command, cancellationToken).ConfigureAwait(false);

        bool containerActionResult;
        try
        {
            containerActionResult = await ExecuteContainerActionAsync(service, command, cancellationToken).ConfigureAwait(false);
        }
        catch (ViixDockerManagerWithHttpStatusCodeException)
        {
            containerActionResult = false;
        }

        await SendContainerNotificationAsync(sendContainerNotifications, command, containerActionResult).ConfigureAwait(false);
    }
}
