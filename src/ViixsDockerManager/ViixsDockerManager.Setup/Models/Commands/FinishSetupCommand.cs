using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Setup.Models.Dtos;

namespace ViixsDockerManager.Setup.Models.Commands;

internal record FinishSetupCommand(Guid SetupId) : CommandBase;
