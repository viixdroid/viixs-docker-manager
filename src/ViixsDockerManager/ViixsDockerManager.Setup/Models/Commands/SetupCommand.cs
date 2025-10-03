using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Setup.Models.Dtos;

namespace ViixsDockerManager.Setup.Models.Commands;

internal record SetupCommand<TCommand>(SetupStep SetupStep, TCommand InternalCommand)
        : ICommand
    where TCommand : ICommand;
