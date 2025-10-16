using Microsoft.Extensions.DependencyInjection;
using ViixsDockerManager.Mediator.Extensions;
using ViixsDockerManager.Setup.Controllers.Hubs;
using ViixsDockerManager.Setup.Handlers.Commands;
using ViixsDockerManager.Setup.Models.Commands;
using ViixsDockerManager.Setup.Services;
using ViixsDockerManager.Setup.Services.Interfaces;
using ViixsDockerManager.Shared.AspNet.Startup;
using ViixsDockerManager.Shared.Extensions;
using ViixsDockerManager.Shared.Models.Commands.DockLightEnvironments;
using ViixsDockerManager.Shared.Models.Commands.Users;
using ViixsDockerManager.Shared.WebSockets;
using ViixsDockerManager.Shared.WebSockets.Interfaces;

namespace ViixsDockerManager.Setup.Startup;

public class SetupServicesComponent : ServiceComponent
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddDecoration<ISendSetupStateNotifications, SendSetupStateNotifications>();
        services.AddSingleton<IConnectionRegistery<SetupInformationHub>, ConnectionRegistery<SetupInformationHub>>();
    }

    protected override void ConfigureCommandHandlers(IServiceCollection services)
    {
        services.RegisterCommandHandler<StartSetupHandler, StartSetupCommand>();

        services.RegisterCommandHandler<CreateUserAccountHandler, CreateFirstUserAccountCommand>();
        services.RegisterCommandHandler<SetupHandler<CreateFirstUserAccountCommand>, SetupCommand<CreateFirstUserAccountCommand>>();

        //services.RegisterCommandHandler<CreateDockLightEnvironmentHandler, CreateDockLightEnvironmentCommand>();
        services.RegisterCommandHandler<SetupHandler<CreateDockLightEnvironmentCommand>, SetupCommand<CreateDockLightEnvironmentCommand>>();
    }
}
