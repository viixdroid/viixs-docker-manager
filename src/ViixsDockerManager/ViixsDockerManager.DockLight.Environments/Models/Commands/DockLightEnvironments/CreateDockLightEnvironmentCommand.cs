using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Shared.Models.Commands.DockLightEnvironments;

namespace ViixsDockerManager.DockLight.Environments.Models.Commands.DockLightEnvironments;

internal record CreateDockLightEnvironmentCommand(string Name, string ApiLocation)
    : CommandBase, ICreateDockLightEnvironmentCommand;
