using ViixsDockerManager.Server.Startup.Factories;
using ViixsDockerManager.Shared.AspNet.Startup.Interfaces;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.Server.Startup;

internal sealed class ServiceStartupFeature(IBuilderCollectionFactory builderCollectionFactory) : StartupFeature
{
    protected override void ConfigureBuilder(WebApplicationBuilder webAppbuilder)
    {
        var servicesBuilderCollection = builderCollectionFactory.GetBuilderCollection<IServiceComponent>();
        servicesBuilderCollection = Guard.ValueIsNotNull(servicesBuilderCollection, nameof(servicesBuilderCollection));
        var components = servicesBuilderCollection.GetComponents();
        foreach (var component in components)
        {
            component.ConfigureServices(webAppbuilder.Services);
        }
    }

    protected override void ConfigureApplication(WebApplication webApp)
    {
        var configureApplicationComponents = builderCollectionFactory.GetBuilderCollection<IConfigureAppComponent>();
        configureApplicationComponents = Guard.ValueIsNotNull(configureApplicationComponents, nameof(configureApplicationComponents));
        var components = configureApplicationComponents.GetComponents();
        foreach (var component in components)
        {
            component.ConfigureApplication(webApp);
        }
    }
}
