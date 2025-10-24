using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Shared.Models.Commands.DockLightEnvironments;

namespace ViixsDockerManager.Setup.Models.Commands;

internal record CreateFirstDockLightEnvironmentCommand(string Name, string ApiLocation)
    : CommandBase, ICreateDockLightEnvironmentCommand;
