using ViixsDockerManager.Shared.Database.Startup.Interfaces;
using ViixsDockerManager.Users.Accounts.DbContexts;

namespace ViixsDockerManager.Users.Accounts.Startup;

public sealed class UserAccountsMigrationComponent : IMigrationComponent
{
    public Type GetMigrationDbContextType() => typeof(UserAccountDbContext);
}
