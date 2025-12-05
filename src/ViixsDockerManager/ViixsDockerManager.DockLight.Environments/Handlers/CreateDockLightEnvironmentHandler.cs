using ViixsDockerManager.DockLight.Environments.Models.Commands.DockLightEnvironments;
using ViixsDockerManager.DockLight.Environments.Services;
using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Shared.Attributes;

namespace ViixsDockerManager.DockLight.Environments.Handlers;

[ViixsController(ControllerName = "DockLightEnvironments2", HttpMethod = "POST")]
internal sealed class CreateDockLightEnvironmentHandler(IDockLightEnvironmentService dockLightEnvironmentService) : ICommandHandler<CreateDockLightEnvironmentCommand>
{
    public Task Handle(CreateDockLightEnvironmentCommand command, CancellationToken cancellationToken = default)
        => dockLightEnvironmentService.CreateDockLightEnvironment(command); //TODO: Add Feedback with websockets
}
