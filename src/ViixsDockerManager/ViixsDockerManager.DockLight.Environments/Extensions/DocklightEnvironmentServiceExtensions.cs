using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ViixsDockerManager.DockLight.Environments.Controllers;
using ViixsDockerManager.DockLight.Environments.DbContexts;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.DockLight.Shared.Handlers;
using ViixsDockerManager.DockLight.Shared.Models.Commands;
using ViixsDockerManager.DockLight.Shared.Models.Queries;
using ViixsDockerManager.Mediator.Extensions;
using ViixsDockerManager.Shared.Database.Extensions;
using ViixsDockerManager.Shared.Database.Sqlite.Extensions;

namespace ViixsDockerManager.DockLight.Environments.Extensions;

public static class DocklightEnvironmentServiceExtensions
{
    public static IServiceCollection AddDockLightEnvironmentServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        //read
        services.AddReadDatabaseServices<DockLightEnvironmentReadDbContext>(configuration);
        services.AddReadEntityServices<DockLightEnvironmentReadDbContext, DockLightEnvironment>();

        //write
        services.AddWriteDatabaseServices<DockLightEnvironmentWriteDbContext>(configuration);
        services.AddWriteEntityServices<DockLightEnvironmentWriteDbContext, DockLightEnvironment>();

        //TODO: Place in correct place.
        services.RegisterCommandHandler<CreateDockLightEnvironmentHandler, CreateDockLightEnvironmentCommand>();
        services.RegisterQueryHandler<GetAllDockLightEnvironmentsHandler, GetAllDockLightEnvironmentsQuery>();

        // services.RegisterQueryHandler<GetAllDockLightEnvironmentsHandler>(typeof(GetAllDockLightEnvironmentsQuery));

        // services.RegisterHandler<>()
        return services;
    }

    public static Task RunDocklightEnvironmentMigrations(this IHost serviceHost)
    {
        return serviceHost.RunMigrations<DockLightEnvironmentWriteDbContext>();
    }

    public static RouteGroupBuilder MapDocklightEnvironmentRoutes(this IEndpointRouteBuilder serviceHost)
    {
        var group = serviceHost.MapGroup("api/docklightenvironments2");

        group.MapRouteActions();
        return group;
    }
}
