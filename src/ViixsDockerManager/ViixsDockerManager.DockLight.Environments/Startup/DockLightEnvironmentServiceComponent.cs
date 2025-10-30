using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ViixsDockerManager.DockLight.Environments.DbContexts;
using ViixsDockerManager.DockLight.Environments.Handlers;
using ViixsDockerManager.DockLight.Environments.Models.Commands.DockLightEnvironments;
using ViixsDockerManager.DockLight.Environments.Models.Queries;
using ViixsDockerManager.DockLight.Environments.Services;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.Mediator.Extensions;
using ViixsDockerManager.Shared.AspNet.Startup;
using ViixsDockerManager.Shared.Database.Extensions;
using ViixsDockerManager.Shared.Database.Models;
using ViixsDockerManager.Shared.Database.Sqlite.Extensions;
using ViixsDockerManager.Shared.Extensions;
using ViixsDockerManager.Shared.Models.Queries.DockLightEnvironments;
using ViixsDockerManager.Shared.Services;

namespace ViixsDockerManager.DockLight.Environments.Startup;

public sealed class DockLightEnvironmentServiceComponent(IConfiguration configuration) : ServiceComponent
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddReadDatabaseServices<DockLightEnvironmentReadDbContext>(configuration);
        services.AddReadEntityServices<DockLightEnvironmentReadDbContext, DockLightEnvironment>();

        services.AddWriteDatabaseServices<DockLightEnvironmentWriteDbContext>(configuration, migrationsHistory: new MigrationsHistory("DockLightEnvironment"));
        services.AddWriteEntityServices<DockLightEnvironmentWriteDbContext, DockLightEnvironment>();

        services.AddDecoration<IDockLightEnvironmentService, DockLightEnvironmentService>();
        services.AddDecoration<ISharedDockLightEnvironmentService, DockLightEnvironmentService>();
    }

    protected override void ConfigureQueryHandlers(IServiceCollection services)
    {
        services.RegisterQueryHandler<GetAllDockLightEnvironmentsHandler, GetAllDockLightEnvironmentsQuery>();
        services.RegisterQueryHandler<GetAllPossibleDockerProtocolsHandler, GetPossibleDockerProtocolsQuery>();
    }

    protected override void ConfigureCommandHandlers(IServiceCollection services)
    {
        services.RegisterCommandHandler<CreateDockLightEnvironmentHandler, CreateDockLightEnvironmentCommand>();
    }
}
