using ViixsDockerManager.Server.Startup.Factories;
using ViixsDockerManager.Server.Startup.Interfaces;
using ViixsDockerManager.Shared.AspNet.Startup.Interfaces;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.Server.Startup;

internal sealed class ConfigureHostStartupFeature(IBuilderCollectionFactory builderCollectionFactory) : IConfigureHostStartupFeature, IStartupFeature
{
    private void ConfigureHost(WebApplicationBuilder webAppbuilder)
    {
        var hostBuilderCollection = builderCollectionFactory.GetBuilderCollection<IHostComponent>();
        hostBuilderCollection = Guard.ValueIsNotNull(hostBuilderCollection, nameof(hostBuilderCollection));
        var components = hostBuilderCollection.GetComponents();
        foreach (var component in components)
        {
            component.ConfigureHost(webAppbuilder.Host);
        }
    }

    void IConfigureHostStartupFeature.ConfigureHost(WebApplicationBuilder webAppbuilder) => ConfigureHost(webAppbuilder);
}
