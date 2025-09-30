using ViixsDockerManager.Server.Startup.Interfaces;

namespace ViixsDockerManager.Server.Startup.Factories;

internal interface IStartupFeatureFactory
{
    IConfigureAppStartupFeature GetConfigureAppStartupFeature();
    IConfigureHostStartupFeature GetConfigureHostStartupFeature();
    IConfigureRoutesStartupFeature GetConfigureRoutesStartupFeature();
    IConfigureServicesStartupFeature GetConfigureServicesStartupFeature();
    IConfigureMigrationStartupFeature GetConfigureMigrationStartupFeature();
}
