using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ViixsDockerManager.Mediator.Extensions;
using ViixsDockerManager.Setup.Controllers.Hubs;
using ViixsDockerManager.Setup.DbContexts;
using ViixsDockerManager.Setup.Handlers.Commands;
using ViixsDockerManager.Setup.Models.Commands;
using ViixsDockerManager.Setup.Models.Entities;
using ViixsDockerManager.Setup.Services;
using ViixsDockerManager.Setup.Services.Interfaces;
using ViixsDockerManager.Shared.AspNet.Startup;
using ViixsDockerManager.Shared.Database.Extensions;
using ViixsDockerManager.Shared.Database.Models;
using ViixsDockerManager.Shared.Database.Sqlite.Extensions;
using ViixsDockerManager.Shared.Extensions;
using ViixsDockerManager.Shared.Models.Commands.DockLightEnvironments;
using ViixsDockerManager.Shared.WebSockets;
using ViixsDockerManager.Shared.WebSockets.Interfaces;

namespace ViixsDockerManager.Setup.Startup;

public class SetupServicesComponent(IConfiguration configuration) : ServiceComponent
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddReadDatabaseServices<SetupReadDbContext>(configuration);
        services.AddReadEntityServices<SetupReadDbContext, SetupState>();

        services.AddWriteDatabaseServices<SetupWriteDbContext>(configuration, migrationsHistory: new MigrationsHistory("Setup"));
        services.AddWriteEntityServices<SetupWriteDbContext, SetupState>();

        services.AddDecoration<ISendSetupStateNotifications, SendSetupStateNotifications>();
        services.AddSingleton<IConnectionRegistery<SetupInformationHub>, ConnectionRegistery<SetupInformationHub>>();
    }

    protected override void ConfigureCommandHandlers(IServiceCollection services)
    {
        services.RegisterCommandHandler<StartSetupHandler, StartSetupCommand>();

        services.RegisterCommandHandler<SetupHandler<SetupStartedCommand>, SetupCommand<SetupStartedCommand>>();
        services.RegisterCommandHandler<SetupStartedHandler, SetupStartedCommand>();

        services.RegisterCommandHandler<CreateUserAccountHandler, CreateFirstUserAccountCommand>();
        services.RegisterCommandHandler<SetupHandler<CreateFirstUserAccountCommand>, SetupCommand<CreateFirstUserAccountCommand>>();

        //services.RegisterCommandHandler<CreateDockLightEnvironmentHandler, CreateDockLightEnvironmentCommand>();
        services.RegisterCommandHandler<SetupHandler<CreateDockLightEnvironmentCommand>, SetupCommand<CreateDockLightEnvironmentCommand>>();
    }
}
