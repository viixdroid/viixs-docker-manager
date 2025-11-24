using ViixsDockerManager.Mediator;
using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Setup.Exceptions;
using ViixsDockerManager.Setup.Models;
using ViixsDockerManager.Setup.Models.Commands;
using ViixsDockerManager.Setup.Models.Dtos;
using ViixsDockerManager.Setup.Models.Entities;
using ViixsDockerManager.Setup.Models.Entities.Filters;
using ViixsDockerManager.Setup.Services;
using ViixsDockerManager.Setup.Services.Interfaces;
using ViixsDockerManager.Shared.Database.Repositories;
using ViixsDockerManager.Shared.Helpers;
using ViixsDockerManager.Shared.Models.Commands.Users;
using ViixsDockerManager.Shared.Attributes;

namespace ViixsDockerManager.Setup.Handlers.Commands;

[ViixsController(typeof(SetupCommand<SetupStartedCommand>), Action = "started", ControllerName = "SetupRoute2", HttpMethod = "POST")]
[ViixsController(typeof(SetupCommand<CreateFirstUserAccountCommand>), Action = "createUser", ControllerName = "SetupRoute2", HttpMethod = "POST")]
[ViixsController(typeof(SetupCommand<CreateFirstDockLightEnvironmentCommand>), Action = "createDockLightEnvironment", ControllerName = "SetupRoute2", HttpMethod = "POST")]
internal sealed class SetupHandler<TCommand>(
    ISetupService setupService,
    ISendSetupStateNotifications sendSetupStateNotifications,
    IMediator mediator
    ) : ICommandHandler<SetupCommand<TCommand>>
    where TCommand : ICommand
{
    public async Task Handle(SetupCommand<TCommand> command, CancellationToken cancellationToken = default)
    {
        var step = (SetupStepName)command.CurrentSetupStepName;
        var nextStep = SetupStepName.GetNextStep(step);
        var currentSetupState = await setupService.GetSetupStateBySetupId(command.SetupId);

        try
        {
            await mediator.Send(command.InternalCommand, cancellationToken);
        }
        catch
        {
            nextStep = step;
        }

        currentSetupState.UpdateSetupState(step, nextStep!);        
        await setupService.UpdateSetupState(currentSetupState);
        await sendSetupStateNotifications.SendNextSetupStepAsync(new SetupStep(currentSetupState.SetupId, nextStep!));
    }
}
