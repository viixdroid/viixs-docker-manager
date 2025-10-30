using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Setup.Models.Commands;

namespace ViixsDockerManager.Setup.Handlers.Commands;

internal sealed class SetupStartedHandler : ICommandHandler<SetupStartedCommand>
{
    public Task Handle(SetupStartedCommand command, CancellationToken cancellationToken = default)
    {
        //Set stated to started in database.
        return Task.CompletedTask;
    }
}
