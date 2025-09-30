using Microsoft.AspNetCore.Builder;
using ViixsDockerManager.DockLight.Environments.DbContexts;
using ViixsDockerManager.Shared.AspNet.Startup;
using ViixsDockerManager.Shared.Database.Sqlite.Extensions;

namespace ViixsDockerManager.DockLight.Environments.Startup;

public sealed class DockLightEnvironmentConfigureAppCompoment : ConfigureAppComponent
{
    protected override void ConfigureApplication(WebApplication webApplication)
    {

    }
}
