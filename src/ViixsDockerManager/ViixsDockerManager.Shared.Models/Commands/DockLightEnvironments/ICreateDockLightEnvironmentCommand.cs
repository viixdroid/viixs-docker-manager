namespace ViixsDockerManager.Shared.Models.Commands.DockLightEnvironments;

public interface ICreateDockLightEnvironmentCommand
{
    string Name { get; }
    string ApiLocation { get; }
}
