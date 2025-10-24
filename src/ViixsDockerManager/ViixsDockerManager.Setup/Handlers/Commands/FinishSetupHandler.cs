using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Setup.Models.Commands;
using ViixsDockerManager.Setup.Services;

namespace ViixsDockerManager.Setup.Handlers.Commands;

internal sealed class FinishSetupHandler(ISetupService setupService) : ICommandHandler<FinishSetupCommand>
{
    public async Task Handle(FinishSetupCommand command, CancellationToken cancellationToken = default)
    {
        var currentSetupState = await setupService.GetSetupStateBySetupId(command.SetupId);

        currentSetupState.FinishSetup();

        await setupService.UpdateSetupState(currentSetupState);
    }
}
