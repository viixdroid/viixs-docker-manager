using ViixsDockerManager.Server.Startup.Factories;
using ViixsDockerManager.Shared.AspNet.Startup.Interfaces;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.Server.Startup;

internal sealed class HostStartupFeature(IBuilderCollectionFactory builderCollectionFactory) : StartupFeature
{
    protected override void ConfigureBuilder(WebApplicationBuilder webAppbuilder)
    {
        var hostBuilderCollection = builderCollectionFactory.GetBuilderCollection<IHostComponent>();
        hostBuilderCollection = Guard.ValueIsNotNull(hostBuilderCollection, nameof(hostBuilderCollection));
        var components = hostBuilderCollection.GetComponents();
        foreach (var component in components)
        {
            component.ConfigureHost(webAppbuilder.Host);
        }

    }
}
