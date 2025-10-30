using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Setup.Models.Commands;
using ViixsDockerManager.Shared.Services;

namespace ViixsDockerManager.Setup.Handlers.Commands;

internal sealed class CreateFirstDockLightEnvironmentHandler(ISharedDockLightEnvironmentService sharedDockLightEnvironmentService) : ICommandHandler<CreateFirstDockLightEnvironmentCommand>
{
    public Task Handle(CreateFirstDockLightEnvironmentCommand command, CancellationToken cancellationToken = default)
    {
        return sharedDockLightEnvironmentService.CreateDockLightEnvironment(command);
    }
}
