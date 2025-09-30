using ViixsDockerManager.Server.Startup.Interfaces;

namespace ViixsDockerManager.Server.Startup.Factories;

internal sealed class StartupFeaturesFactory(IConfiguration configuration) : IStartupFeatureFactory
{
    private readonly IBuilderCollectionFactory _builderCollectionFactory = new BuilderCollectionFactory(configuration);

    public IConfigureMigrationStartupFeature GetConfigureMigrationStartupFeature() => new ConfigureMigrationsStartupFeature(_builderCollectionFactory);
    public IConfigureAppStartupFeature GetConfigureAppStartupFeature() => new ConfigureAppStartupFeature(_builderCollectionFactory);
    public IConfigureServicesStartupFeature GetConfigureServicesStartupFeature() => new ConfigureServiceStartupFeature(_builderCollectionFactory);
    public IConfigureRoutesStartupFeature GetConfigureRoutesStartupFeature() => new ConfigureRoutesStartupFeature(_builderCollectionFactory);
    public IConfigureHostStartupFeature GetConfigureHostStartupFeature() => new ConfigureHostStartupFeature(_builderCollectionFactory);

}
