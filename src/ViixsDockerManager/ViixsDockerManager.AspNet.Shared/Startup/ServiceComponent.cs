using Microsoft.Extensions.DependencyInjection;
using ViixsDockerManager.Shared.AspNet.Startup.Interfaces;

namespace ViixsDockerManager.Shared.AspNet.Startup;

public abstract class ServiceComponent : IServiceComponent
{
    protected virtual void ConfigureCommandHandlers(IServiceCollection services) { }
    protected virtual void ConfigureQueryHandlers(IServiceCollection services) { }
    protected abstract void ConfigureServices(IServiceCollection services);

    void IServiceComponent.ConfigureServices(IServiceCollection services)
    {
        ConfigureServices(services);
        ConfigureCommandHandlers(services);
        ConfigureQueryHandlers(services);
    }
}
