using ViixsDockerManager.Shared.Models.Commands.DockLightEnvironments;

namespace ViixsDockerManager.Shared.Services;

public interface ISharedDockLightEnvironmentService
{
    Task CreateDockLightEnvironment(ICreateDockLightEnvironmentCommand createDockLightEnvironmentCommand);
}
