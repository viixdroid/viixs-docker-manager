using ViixsDockerManager.DockLight.Environments.Startup;
using ViixsDockerManager.Server.Startup.Builders;
using ViixsDockerManager.Shared.Database.Startup.Interfaces;
using ViixsDockerManager.Users.Accounts.Startup;

namespace ViixsDockerManager.Server.Startup.Collections;

internal class MigrationComponentCollection : BuilderCollection<IMigrationComponent>
{
    protected override IReadOnlyList<IMigrationComponent> GetComponents()
        => [
            new DockLightEnvironmentMigrationComponent(),
            new UserAccountsMigrationComponent()
            ];
}
