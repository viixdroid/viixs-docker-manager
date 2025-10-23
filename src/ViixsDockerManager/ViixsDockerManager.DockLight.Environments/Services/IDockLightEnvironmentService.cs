using ViixsDockerManager.Shared.Models.Commands.DockLightEnvironments;
using ViixsDockerManager.Shared.Services;

namespace ViixsDockerManager.DockLight.Environments.Services;

internal interface IDockLightEnvironmentService : IViixsBaseService
{
    Task CreateDockLightEnvironment(ICreateDockLightEnvironmentCommand createDockLightEnvironmentCommand);
}
