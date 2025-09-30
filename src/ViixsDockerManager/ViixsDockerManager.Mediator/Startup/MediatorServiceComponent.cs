using Microsoft.Extensions.DependencyInjection;
using ViixsDockerManager.Mediator.Extensions;
using ViixsDockerManager.Shared.AspNet.Startup;

namespace ViixsDockerManager.Mediator.Startup;

public class MediatorServiceComponent : ServiceComponent
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddMediatorServices();
    }
}
