using ViixsDockerManager.Mediator.Commands;

namespace ViixsDockerManager.Setup.Models.Commands;

internal record SetupCommand<TCommand>(Guid SetupId, TCommand InternalCommand, string CurrentSetupStepName)
        : ICommand
    where TCommand : ICommand;
