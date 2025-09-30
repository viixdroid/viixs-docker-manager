using ViixsDockerManager.Shared.Database.Migrations;

namespace ViixsDockerManager.Server.Startup.Interfaces;

public interface IConfigureMigrationStartupFeature
{
    void GetMigrations(IMigrationRunner migrationRunner);
}
