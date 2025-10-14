using ViixsDockerManager.Mediator.Commands;

namespace ViixsDockerManager.DockLight.Environments.Models.Commands;

public record CreateDockLightEnvironmentCommand(string Name, string ApiLocation) : CommandBase
{
    //TODO: remove
    public override string ToString() => $"{Name} -> {ApiLocation}";
};
