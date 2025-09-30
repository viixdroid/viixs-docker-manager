using ViixsDockerManager.Server.Startup.Factories;
using ViixsDockerManager.Server.Startup.Interfaces;
using ViixsDockerManager.Shared.Database.Migrations;
using ViixsDockerManager.Shared.Database.Startup.Interfaces;
using ViixsDockerManager.Shared.Exceptions;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.Server.Startup;

internal sealed class ConfigureMigrationsStartupFeature(IBuilderCollectionFactory builderCollectionFactory) : IConfigureMigrationStartupFeature, IStartupFeature
{
    private void GetMigrations(IMigrationRunner migrationRunner)
    {
        var migrationComponentsCollection = builderCollectionFactory.GetBuilderCollection<IMigrationComponent>();
        migrationComponentsCollection = Guard.ValueIsNotNull(migrationComponentsCollection, nameof(migrationComponentsCollection));
        var migrationComponents = migrationComponentsCollection.GetComponents();
        foreach (var component in migrationComponents)
        {
            migrationRunner.AddContext(component.GetMigrationDbContextType());
        }
    }

    void IConfigureMigrationStartupFeature.GetMigrations(IMigrationRunner migrationRunner) => GetMigrations(migrationRunner);
}
