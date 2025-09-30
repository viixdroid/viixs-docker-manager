using ViixsDockerManager.Server.Components;
using ViixsDockerManager.Server.Startup.Builders;
using ViixsDockerManager.Shared.AspNet.Startup.Interfaces;
using ViixsDockerManager.Users.Accounts.Startup;

namespace ViixsDockerManager.Server.Startup.Collections;

internal sealed class ConfigureApplicationComponentCollection(IConfiguration configuration)
    : BuilderCollection<IConfigureAppComponent>
{
    protected override IReadOnlyList<IConfigureAppComponent> GetComponents()
        => [
            new UserAccountsConfigureAppComponent(),
            new WebAppConfigureAppComponent() //this one must be last.
            ];
}
