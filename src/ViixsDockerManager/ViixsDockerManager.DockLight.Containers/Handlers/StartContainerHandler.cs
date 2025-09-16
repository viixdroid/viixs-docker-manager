using ViixsDockerManager.DockLight.Models.Commands;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Shared.Exceptions;

namespace ViixsDockerManager.DockLight.Handlers;

internal class StartContainerHandler(IDockerServiceFactory dockerServiceFactory, ISendContainerNotifications sendContainerNotifications) : DockLightHandlerBase(dockerServiceFactory), ICommandHandler<StartContainerCommand>
{
    public async Task Handle(StartContainerCommand command, CancellationToken cancellationToken = default)
    {
        var service = await GetDockerContainerService(command, cancellationToken).ConfigureAwait(false);

        try
        {
            var startResult = await service.StartContainerAsync(command.ContainerId, cancellationToken).ConfigureAwait(false);
            await sendContainerNotifications.SendContainerStarted(command, startResult).ConfigureAwait(false);
        }
        catch (ViixDockerManagerWithHttpStatusCodeException)
        {
            await sendContainerNotifications.SendContainerStarted(command, false).ConfigureAwait(false);
        }
    }
}
