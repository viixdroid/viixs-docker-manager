using ViixsDockerManager.Setup.DbContexts;
using ViixsDockerManager.Shared.Database.Startup.Interfaces;

namespace ViixsDockerManager.Setup.Startup;

public sealed class SetupMigrationComponent : IMigrationComponent
{
    public Type GetMigrationDbContextType() => typeof(SetupWriteDbContext);
}
