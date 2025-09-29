using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ViixsDockerManager.DockLight.Environments.Controllers;
using ViixsDockerManager.DockLight.Environments.DbContexts;
using ViixsDockerManager.DockLight.Environments.Handlers;
using ViixsDockerManager.DockLight.Environments.Models.Commands;
using ViixsDockerManager.DockLight.Environments.Models.Queries;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.Mediator.Extensions;
using ViixsDockerManager.Shared.Database.Extensions;
using ViixsDockerManager.Shared.Database.Models;
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
        services.AddWriteDatabaseServices<DockLightEnvironmentWriteDbContext>(configuration, migrationsHistory: new MigrationsHistory("DockLightEnvironment"));
        services.AddWriteEntityServices<DockLightEnvironmentWriteDbContext, DockLightEnvironment>();

        //TODO: Place in correct place.
        services.RegisterCommandHandler<CreateDockLightEnvironmentHandler, CreateDockLightEnvironmentCommand>();
        services.RegisterQueryHandler<GetAllDockLightEnvironmentsHandler, GetAllDockLightEnvironmentsQuery>();
        services.RegisterQueryHandler<GetAllPossibleDockerProtocolsHandler, GetPossibleDockerProtocolsQuery>();

        // services.RegisterQueryHandler<GetAllDockLightEnvironmentsHandler>(typeof(GetAllDockLightEnvironmentsQuery));

        // services.RegisterHandler<>()
        return services;
    }

    //public static void AddDocklightEnvironmentMigrations(this IHost serviceHost)
    //    => serviceHost.AddContextForMigrationRunning<DockLightEnvironmentWriteDbContext>();

    public static RouteGroupBuilder MapDocklightEnvironmentRoutes(this IEndpointRouteBuilder serviceHost)
    {
        var group = serviceHost.MapGroup("docklightenvironments");

        group.MapRouteActions();
        return group;
    }

    public static RouteGroupBuilder MapDockLightEnvironmentSetupRoutes(this IEndpointRouteBuilder serviceHost)
    {
        var group = serviceHost.MapGroup("setup");

        group.MapDockLightEnvironmentSetupRouteActions();
        return group;
    }
}
