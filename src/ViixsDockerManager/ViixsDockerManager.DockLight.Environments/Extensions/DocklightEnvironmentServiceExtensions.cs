using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ViixsDockerManager.DockLight.Environments.Controllers;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.Shared.Database.Extensions;
using ViixsDockerManager.Shared.Database.Sqlite.Extensions;

namespace ViixsDockerManager.DockLight.Environments.Extensions;

public static class DocklightEnvironmentServiceExtensions
{
    public static IServiceCollection AddDockLightEnvironmentServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddReadDatabaseServices<DocklightEnvironmentReadDbContext>(configuration);
        services.AddEntityServices<DocklightEnvironmentReadDbContext, DocklightEnvironment>();
        return services;
    }

    public static Task RunDocklightEnvironmentMigrations(this IHost serviceHost)
    {
        return serviceHost.RunMigrations<DocklightEnvironmentReadDbContext>();
    }

    public static RouteGroupBuilder MapDocklightEnvironmentRoutes(this IEndpointRouteBuilder serviceHost)
    {
        var group = serviceHost.MapGroup("api/docklightenvironments2");

        group.MapRouteActions();
        return group;
    }
}
