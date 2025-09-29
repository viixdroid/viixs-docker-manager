using Microsoft.AspNetCore.Builder;
using ViixsDockerManager.Shared.Database.Migrations;

namespace ViixsDockerManager.Server.Startup;

internal abstract class StartupFeature : IStartupFeature
{
    protected virtual void ConfigureApplication(WebApplication webApp)
    {
        
    }

    protected virtual void GetMigrations(IMigrationRunner migrationRunner)
    {

    }

    protected abstract void ConfigureBuilder(WebApplicationBuilder webAppbuilder);

    void IStartupFeature.ConfigureApplication(WebApplication webApp) => ConfigureApplication(webApp);
    void IStartupFeature.ConfigureBuilder(WebApplicationBuilder webAppbuilder) => ConfigureBuilder(webAppbuilder);
    void IStartupFeature.GetMigrations(IMigrationRunner migrationRunner) => GetMigrations(migrationRunner);
}
