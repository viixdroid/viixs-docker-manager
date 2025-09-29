using ViixsDockerManager.Shared.AspNet.Startup.Interfaces;
using ViixsDockerManager.Shared.Startup.Interfaces;

namespace ViixsDockerManager.Server.Startup.Builders;

internal interface IBuilderCollection
{
}

internal interface IBuilderCollection<out TBuilderComponent> : IBuilderCollection
    where TBuilderComponent : class, IApplicationBuilderComponent
{
    IReadOnlyList<TBuilderComponent> GetComponents();
}
