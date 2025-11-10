using ViixsDockerManager.DockLight.Environments.Startup;
using ViixsDockerManager.DockLight.Startup;
using ViixsDockerManager.Mediator.Startup;
using ViixsDockerManager.Server.Components;
using ViixsDockerManager.Server.Startup.Builders;
using ViixsDockerManager.Setup.Startup;
using ViixsDockerManager.Shared.AspNet.Startup.Interfaces;
using ViixsDockerManager.Users.Accounts.Startup;
using ViixsDockerManager.Version.Startup;

namespace ViixsDockerManager.Server.Startup.Collections;

internal class ServicesComponentCollection(IConfiguration configuration)
    : BuilderCollection<IServiceComponent>
{
    protected override IReadOnlyList<IServiceComponent> GetComponents()
        => [
            new MediatorServiceComponent(),
            new DockLightEnvironmentServiceComponent(configuration),
            new DockLightContainerServiceComponent(),
            new UserAccountsServiceComponent(configuration),
            new SetupServicesComponent(configuration),
            new VersionServiceComponent(),
            new WebAppServiceComponent()
            ];
}
