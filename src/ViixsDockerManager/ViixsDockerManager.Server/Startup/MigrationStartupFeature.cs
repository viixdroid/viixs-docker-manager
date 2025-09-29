using ViixsDockerManager.Server.Startup.Factories;
using ViixsDockerManager.Shared.Database.Migrations;
using ViixsDockerManager.Shared.Database.Startup.Interfaces;
using ViixsDockerManager.Shared.Exceptions;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.Server.Startup;

internal sealed class MigrationStartupFeature(IBuilderCollectionFactory builderCollectionFactory) : StartupFeature
{
    protected override void ConfigureBuilder(WebApplicationBuilder webAppbuilder) { }

    protected override void GetMigrations(IMigrationRunner migrationRunner)
    {
        var migrationComponentsCollection = builderCollectionFactory.GetBuilderCollection<IMigrationComponent>();
        migrationComponentsCollection = Guard.ValueIsNotNull(migrationComponentsCollection, nameof(migrationComponentsCollection));
        var migrationComponents = migrationComponentsCollection.GetComponents();
        foreach (var component in migrationComponents)
        {
            migrationRunner.AddContext(component.GetMigrationDbContextType());
        }
    }
}
