using ViixsDockerManager.Server.Startup.Builders;
using ViixsDockerManager.Shared.Startup.Interfaces;

namespace ViixsDockerManager.Server.Startup.Factories;

internal interface IBuilderCollectionFactory
{
    IBuilderCollection<TBuilderComponent>? GetBuilderCollection<TBuilderComponent>()
        where TBuilderComponent : class, IApplicationBuilderComponent;
}
