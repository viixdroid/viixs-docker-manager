using ViixsDockerManager.Server.Startup.Factories;
using ViixsDockerManager.Server.Startup.Interfaces;
using ViixsDockerManager.Shared.AspNet.Startup.Interfaces;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.Server.Startup;

internal sealed class ConfigureServiceStartupFeature(IBuilderCollectionFactory builderCollectionFactory) : IConfigureServicesStartupFeature, IStartupFeature
{
    private void ConfigureServices(WebApplicationBuilder webAppbuilder)
    {
        var servicesBuilderCollection = builderCollectionFactory.GetBuilderCollection<IServiceComponent>();
        servicesBuilderCollection = Guard.ValueIsNotNull(servicesBuilderCollection, nameof(servicesBuilderCollection));
        var components = servicesBuilderCollection.GetComponents();
        foreach (var component in components)
        {
            component.ConfigureServices(webAppbuilder.Services);
        }
    }

    void IConfigureServicesStartupFeature.ConfigureServices(WebApplicationBuilder webAppbuilder) => ConfigureServices(webAppbuilder);
}
