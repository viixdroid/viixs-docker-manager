using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ViixsDockerManager.Mediator;
using ViixsDockerManager.Setup.Models.Commands;
using ViixsDockerManager.Setup.Models.Dtos;
using ViixsDockerManager.Setup.Models.Queries;
using ViixsDockerManager.Shared.Models.Commands.DockLightEnvironments;
using ViixsDockerManager.Shared.Models.Commands.Users;
using ViixsDockerManager.Shared.Models.Dtos.DockLightEnvironments;
using ViixsDockerManager.Shared.Models.Queries.DockLightEnvironments;

namespace ViixsDockerManager.Setup.Controllers;

internal static class SetupRouteActions
{
    public static RouteGroupBuilder MapSetupRouteActions(this RouteGroupBuilder builder)
    {
        builder.MapGet("/issetupfinished", GetIsSetupDone);

        builder.MapGet("/docklight/configuration", GetDockLightProtocols);

        builder.MapPost("/started", SetSetupStarted);

        builder.MapPost("/start", StartSetup);
        builder.MapPost("/createuser", CreateUserAccount);
        builder.MapPost("/createdocklightenvironment", CreateDockLightEnvironment);
        builder.MapPost("/finish", FinishSetup);
        return builder;
    }


    private static Task<IsSetupFinished> GetIsSetupDone([FromServices] IMediator mediator)
    {
        var query = new IsSetupFinishedQuery();
        return mediator.Send(query);
    }

    private static Task<DockLightEnvironmentConfig> GetDockLightProtocols([FromServices] IMediator mediator)
    {
        var query = new GetPossibleDockerProtocolsQuery();
        return mediator.Send(query);
    }

    private static Task StartSetup([FromBody] StartSetupCommand startSetupCommand, [FromServices] IMediator mediator)
    {
        return mediator.Send(startSetupCommand);
    }
    private static Task SetSetupStarted([FromBody] SetupCommand<SetupStartedCommand> setupStartedCommand, [FromServices] IMediator mediator)
    {
        return mediator.Send(setupStartedCommand);
    }

    private static Task CreateUserAccount([FromBody] SetupCommand<CreateFirstUserAccountCommand> createUserAccountCommand, [FromServices] IMediator mediator)
    {
        return mediator.Send(createUserAccountCommand);
    }

    private static Task CreateDockLightEnvironment([FromBody] SetupCommand<CreateFirstDockLightEnvironmentCommand> createDockLightEnvironmentCommand, [FromServices] IMediator mediator)
    {
        return mediator.Send(createDockLightEnvironmentCommand);
    }

    private static Task FinishSetup([FromBody] FinishSetupCommand finishSetupCommand, [FromServices] IMediator mediator)
    {        
        return mediator.Send(finishSetupCommand);
    }
}
