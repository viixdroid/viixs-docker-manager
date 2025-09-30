using ViixsDockerManager.DockLight.Environments.DbContexts;
using ViixsDockerManager.Shared.Database.Startup.Interfaces;

namespace ViixsDockerManager.DockLight.Environments.Startup;

public class DockLightEnvironmentMigrationComponent : IMigrationComponent
{
    public Type GetMigrationDbContextType() => typeof(DockLightEnvironmentWriteDbContext);
}
