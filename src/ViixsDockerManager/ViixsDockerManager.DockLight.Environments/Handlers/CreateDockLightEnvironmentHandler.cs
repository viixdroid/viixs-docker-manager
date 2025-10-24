using ViixsDockerManager.DockLight.Environments.Models.Commands.DockLightEnvironments;
using ViixsDockerManager.DockLight.Environments.Services;
using ViixsDockerManager.Mediator.Commands;

namespace ViixsDockerManager.DockLight.Environments.Handlers;

internal sealed class CreateDockLightEnvironmentHandler(IDockLightEnvironmentService dockLightEnvironmentService) : ICommandHandler<CreateDockLightEnvironmentCommand>
{
    public Task Handle(CreateDockLightEnvironmentCommand command, CancellationToken cancellationToken = default)
        => dockLightEnvironmentService.CreateDockLightEnvironment(command); //TODO: Add Feedback with websockets
}
