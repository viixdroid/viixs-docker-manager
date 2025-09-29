
namespace ViixsDockerManager.Server.Startup.Factories;

internal interface IStartupFeatureFactory
{
    IEnumerable<IStartupFeature> GetStartupFeatures();
}