using ViixsDockerManager.Server.Startup.Factories;
using ViixsDockerManager.Server.Startup.Interfaces;
using ViixsDockerManager.Shared.AspNet.Startup.Interfaces;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.Server.Startup;

internal sealed class ConfigureAppStartupFeature(IBuilderCollectionFactory builderCollectionFactory) : IConfigureAppStartupFeature, IStartupFeature
{
    private void ConfigureApplication(WebApplication webApp)
    {
        var configureApplicationComponents = builderCollectionFactory.GetBuilderCollection<IConfigureAppComponent>();
        configureApplicationComponents = Guard.ValueIsNotNull(configureApplicationComponents, nameof(configureApplicationComponents));
        var components = configureApplicationComponents.GetComponents();
        foreach (var component in components)
        {
            component.ConfigureApplication(webApp);
        }
    }

    void IConfigureAppStartupFeature.ConfigureApplication(WebApplication webApp) => ConfigureApplication(webApp);
}
