using ViixsDockerManager.Mediator.Commands;

namespace ViixsDockerManager.DockLight.Environments.Models.Commands;

public record CreateDockLightEnvironmentCommand(string Name, string ApiLocation) : ICommand
{
    //TODO: remove
    public override string ToString() => $"{Name} -> {ApiLocation}";
};
