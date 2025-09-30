using ViixsDockerManager.Shared.AspNet.Startup.Interfaces;
using ViixsDockerManager.Shared.Startup.Interfaces;

namespace ViixsDockerManager.Server.Startup.Builders;

internal abstract class BuilderCollection<TBuilderComponent> : IBuilderCollection<TBuilderComponent>
    where TBuilderComponent : class, IApplicationBuilderComponent
{
    protected abstract IReadOnlyList<TBuilderComponent> GetComponents();
    IReadOnlyList<TBuilderComponent> IBuilderCollection<TBuilderComponent>.GetComponents() => GetComponents();
}
