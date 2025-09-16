using ViixsDockerManager.DockLight.Models.Commands;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.Mediator.Commands;

namespace ViixsDockerManager.DockLight.Handlers;

internal class KillContainerHandler(IDockerServiceFactory dockerServiceFactory, ISendContainerNotifications sendContainerNotifications) : DockLightHandlerBase(dockerServiceFactory), ICommandHandler<StartContainerCommand>
{
    public async Task Handle(StartContainerCommand command, CancellationToken cancellationToken = default)
    {
        var service = await GetDockerContainerService(command, cancellationToken).ConfigureAwait(false);

        await service.KillContainerAsync(command.ContainerId, cancellationToken: cancellationToken).ConfigureAwait(false);
        await sendContainerNotifications.SendContainerKilled(command).ConfigureAwait(false);
    }
}
