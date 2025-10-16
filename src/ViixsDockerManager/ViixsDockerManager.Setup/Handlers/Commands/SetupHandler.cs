using ViixsDockerManager.Mediator;
using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Setup.Exceptions;
using ViixsDockerManager.Setup.Models;
using ViixsDockerManager.Setup.Models.Commands;
using ViixsDockerManager.Setup.Models.Entities;
using ViixsDockerManager.Setup.Models.Entities.Filters;
using ViixsDockerManager.Setup.Services.Interfaces;
using ViixsDockerManager.Shared.Database.Repositories;
using ViixsDockerManager.Shared.Helpers;
using ViixsDockerManager.Shared.Models.Commands.Users;

namespace ViixsDockerManager.Setup.Handlers.Commands;

internal sealed class SetupHandler<TCommand>(
    IDatabaseReadRepository<SetupState> setupStateReadRepository,
    ISendSetupStateNotifications sendSetupStateNotifications,
    IMediator mediator
    ) : ICommandHandler<SetupCommand<TCommand>>
    where TCommand : ICommand
{
    public async Task Handle(SetupCommand<TCommand> command, CancellationToken cancellationToken = default)
    {
        var setupId = Guard.ValueIsNotNull(command.SetupId, nameof(command.SetupId));
        var currentStep = await setupStateReadRepository.GetByFilterAsync(new GetStateBySetupIdFilter(setupId));

        if (currentStep is null)
        {
            throw new SetupIdDoesNotExistException(setupId);
        }

        var step = (SetupStepName)command.CurrentSetupStepName;

        await mediator.Send(command.InternalCommand, cancellationToken);
        await sendSetupStateNotifications.SendNextSetupStepAsync(new Models.Dtos.SetupStep(setupId, SetupStepName.GetNextStep(step)!));
    }
}
