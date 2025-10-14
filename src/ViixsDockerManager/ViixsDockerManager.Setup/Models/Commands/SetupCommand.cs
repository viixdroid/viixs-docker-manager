using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Setup.Exceptions;
using ViixsDockerManager.Setup.Models.Dtos;

namespace ViixsDockerManager.Setup.Models.Commands;

internal record SetupCommand<TCommand>(Guid SetupId, TCommand InternalCommand, string CurrentSetupStepName)
        : ICommand
    where TCommand : ICommand;


internal record SetupCommand(Guid SetupId, ICommand InternalCommand)
        : CommandBase;
