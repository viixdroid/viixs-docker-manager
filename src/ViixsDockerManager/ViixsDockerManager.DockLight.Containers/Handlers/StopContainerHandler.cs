using ViixsDockerManager.DockLight.Models.Commands;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.Mediator.Commands;

namespace ViixsDockerManager.DockLight.Handlers;

internal class StopContainerHandler(IDockerServiceFactory dockerServiceFactory, ISendContainerNotifications sendContainerNotifications) : DockLightHandlerBase(dockerServiceFactory), ICommandHandler<StopContainerCommand>
{
    public async Task Handle(StopContainerCommand command, CancellationToken cancellationToken = default)
    {
        var service = await GetDockerContainerService(command, cancellationToken).ConfigureAwait(false);

        await service.StopContainerAsync(command.ContainerId, cancellationToken: cancellationToken).ConfigureAwait(false);
        await sendContainerNotifications.SendContainerStopped(command).ConfigureAwait(false);
    }
}
