using Microsoft.AspNetCore.Builder;
using ViixsDockerManager.Shared.Database.Migrations;

namespace ViixsDockerManager.Server.Startup;

public interface IStartupFeature
{
    void ConfigureBuilder(WebApplicationBuilder webAppbuilder);
    void ConfigureApplication(WebApplication webApp);
    void GetMigrations(IMigrationRunner migrationRunner);
}
