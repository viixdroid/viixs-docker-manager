using Microsoft.Extensions.DependencyInjection;
using ViixsDockerManager.Shared.Startup.Interfaces;

namespace ViixsDockerManager.Shared.AspNet.Startup.Interfaces;

public interface IServiceComponent : IApplicationBuilderComponent
{
    void ConfigureServices(IServiceCollection services);
}
