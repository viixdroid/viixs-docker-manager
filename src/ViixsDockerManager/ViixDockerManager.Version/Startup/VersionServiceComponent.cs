using Microsoft.Extensions.DependencyInjection;
using ViixsDockerManager.Mediator.Extensions;
using ViixsDockerManager.Shared.AspNet.Startup;
using ViixsDockerManager.Version.Handlers;
using ViixsDockerManager.Version.Queries;

namespace ViixsDockerManager.Version.Startup;

public sealed class VersionServiceComponent : ServiceComponent
{
    protected override void ConfigureQueryHandlers(IServiceCollection services)
    {
        services.RegisterQueryHandler<VersionRequestHandler, VersionRequestQuery>();
    }

    protected override void ConfigureServices(IServiceCollection services)
    {

    }
}
