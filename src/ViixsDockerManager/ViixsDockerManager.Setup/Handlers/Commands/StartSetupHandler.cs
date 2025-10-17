using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Setup.Models;
using ViixsDockerManager.Setup.Models.Commands;
using ViixsDockerManager.Setup.Models.Entities;
using ViixsDockerManager.Setup.Models.Entities.Filters;
using ViixsDockerManager.Setup.Services.Interfaces;
using ViixsDockerManager.Shared.Database.Repositories;

namespace ViixsDockerManager.Setup.Handlers.Commands;

internal class StartSetupHandler(
    IDatabaseReadRepository<SetupState> setupStateReadRepository,
    IDatabaseWriteRepository<SetupState> setupStateWriteRepository,
    ISendSetupStateNotifications sendSetupStateNotifications
    ) : ICommandHandler<StartSetupCommand>
{
    public async Task Handle(StartSetupCommand command, CancellationToken cancellationToken = default)
    {
        var currentSetupState = await setupStateReadRepository.GetFirstOrDefaultAsync();
        if (currentSetupState is null)
        {
            currentSetupState = new SetupState()
            {
                SetupId = Guid.NewGuid(),
                CurrentStep = SetupStepName.Welcome,
                LastUpdated = DateTime.UtcNow,
                IsCompleted = false,
            };

            await setupStateWriteRepository.Save(currentSetupState);
        }
        await sendSetupStateNotifications.SendOnSetupStartedAsync(command.ConnectionId, currentSetupState);
    }
}
