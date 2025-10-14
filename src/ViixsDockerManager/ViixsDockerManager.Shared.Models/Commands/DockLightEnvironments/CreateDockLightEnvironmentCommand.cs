using ViixsDockerManager.Mediator.Commands;

namespace ViixsDockerManager.Shared.Models.Commands.DockLightEnvironments;

public record CreateDockLightEnvironmentCommand(string Name, string ApiLocation) : CommandBase
{
    //TODO: remove
    public override string ToString() => $"{Name} -> {ApiLocation}";
};
