using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Setup.Models.Dtos;

namespace ViixsDockerManager.Setup.Models.Commands;

internal record StartSetupCommand(string ConnectionId, Guid? SetupId = null) : ICommand;
