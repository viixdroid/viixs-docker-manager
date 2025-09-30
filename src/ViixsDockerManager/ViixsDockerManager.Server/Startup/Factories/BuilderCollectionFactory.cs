using ViixsDockerManager.Server.Startup.Builders;
using ViixsDockerManager.Server.Startup.Collections;
using ViixsDockerManager.Shared.AspNet.Startup.Interfaces;
using ViixsDockerManager.Shared.Database.Startup.Interfaces;

namespace ViixsDockerManager.Server.Startup.Factories;

internal class BuilderCollectionFactory(IConfiguration configuration) : IBuilderCollectionFactory
{
    private static readonly Dictionary<Type, Func<IConfiguration, IBuilderCollection>> _builderRegistery = new Dictionary<Type, Func<IConfiguration, IBuilderCollection>>()
    {
        { typeof(IHostComponent), (configuration) => new HostComponentCollection(configuration) },
        { typeof(IServiceComponent), (configuration) => new ServicesComponentCollection(configuration) },
        { typeof(IConfigureAppComponent), (configuration) => new ConfigureApplicationComponentCollection(configuration) },
        { typeof(IRouteComponent), _ => new RouteComponentCollection() },
        { typeof(IMigrationComponent), _ => new MigrationComponentCollection() }
    };

    IBuilderCollection<TBuilderComponent>? IBuilderCollectionFactory.GetBuilderCollection<TBuilderComponent>()
    {
        var componentType = typeof(TBuilderComponent);
        if (!_builderRegistery.TryGetValue(componentType, out var builderFunc))
        {
            return null;
        }
        var specificCollection = builderFunc(configuration);
        return specificCollection as IBuilderCollection<TBuilderComponent>;
    }
}
