using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Setup.Models;
using ViixsDockerManager.Setup.Models.Commands;
using ViixsDockerManager.Setup.Models.Entities;
using ViixsDockerManager.Setup.Models.Entities.Filters;
using ViixsDockerManager.Setup.Services;
using ViixsDockerManager.Setup.Services.Interfaces;
using ViixsDockerManager.Shared.Database.Repositories;

namespace ViixsDockerManager.Setup.Handlers.Commands;

internal class StartSetupHandler(
    ISetupService setupService,
    ISendSetupStateNotifications sendSetupStateNotifications
    ) : ICommandHandler<StartSetupCommand>
{
    public async Task Handle(StartSetupCommand command, CancellationToken cancellationToken = default)
    {
        var currentSetupState = await setupService.GetFirstSetupState();
        if (currentSetupState is null)
        {
            currentSetupState = new SetupState()
            {
                SetupId = Guid.NewGuid(),
                CurrentStep = SetupStepName.Welcome,
                LastUpdated = DateTime.UtcNow,
                IsCompleted = false,
            };
            await setupService.SaveSetupState(currentSetupState);
        }
        await sendSetupStateNotifications.SendOnSetupStartedAsync(command.ConnectionId, currentSetupState);
    }
}
