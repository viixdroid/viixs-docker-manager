
namespace ViixsDockerManager.Server.Startup.Factories;

internal sealed class StartupFeatureFactory(IConfiguration configuration) : IStartupFeatureFactory
{
    private readonly IBuilderCollectionFactory _builderCollectionFactory = new BuilderCollectionFactory(configuration);
    public IEnumerable<IStartupFeature> GetStartupFeatures()
    {
        yield return new HostStartupFeature(_builderCollectionFactory);
        yield return new ServiceStartupFeature(_builderCollectionFactory);
        yield return new RouteStartupFeature(_builderCollectionFactory);
        yield return new MigrationStartupFeature(_builderCollectionFactory);
    }
}
