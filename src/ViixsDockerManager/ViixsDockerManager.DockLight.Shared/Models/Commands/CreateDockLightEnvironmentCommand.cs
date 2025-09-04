using ViixsDockerManager.Mediator.Commands;

namespace ViixsDockerManager.DockLight.Shared.Models.Commands;

public record CreateDockLightEnvironmentCommand(string Name, string ApiLocation) : ICommand
{
    //TODO: remove
    public override string ToString() => $"{Name} -> {ApiLocation}";
};
