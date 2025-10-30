using ViixsDockerManager.Mediator.Commands;

namespace ViixsDockerManager.Setup.Models.Commands;

internal record SetupStartedCommand(Guid SetupId)
    : CommandBase;
