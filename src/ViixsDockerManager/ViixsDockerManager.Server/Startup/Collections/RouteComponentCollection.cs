using ViixsDockerManager.DockLight.Environments.Startup;
using ViixsDockerManager.DockLight.Startup;
using ViixsDockerManager.Server.Startup.Builders;
using ViixsDockerManager.Shared.AspNet.Startup.Interfaces;
using ViixsDockerManager.Users.Accounts.Startup;

namespace ViixsDockerManager.Server.Startup.Collections;

internal sealed class RouteComponentCollection : IBuilderCollection<IRouteComponent>
{
    public IReadOnlyList<IRouteComponent> GetComponents()
        => [
            new DockLightEnvironmentRouteComponent(),
            new DockLightContainerRouteComponent(),
            new UserAccountsRouteComponent()
            ];
}
