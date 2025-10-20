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
        builder.MapGet("/issetupdone", GetIsSetupDone);

        builder.MapGet("/currentStep", GetCurrentSetupStep);
        builder.MapPost("/currentStep", SetCurrentSetupStep);

        builder.MapGet("/docklight/configuration", GetDockLightProtocols);

        builder.MapPost("/updateStep", UpdateStep);

        builder.MapPost("/started", SetSetupStarted);

        builder.MapPost("/start", StartSetup);
        builder.MapPost("/createuser", CreateUserAccount);
        builder.MapPost("/createdocklightenvironment", CreateDockLightEnvironment);
        builder.MapPost("/finish", FinishSetup);
        return builder;
    }


    private static Task<IsSetupDone> GetIsSetupDone([FromServices] IMediator mediator)
    {
        var query = new IsSetupDoneQuery();
        return mediator.Send(query);
    }

    private static Task SetCurrentSetupStep([FromBody] SetCurrentSetupStepCommand setCurrentSetupStepCommand, [FromServices] IMediator mediator)
    {
        return mediator.Send(setCurrentSetupStepCommand);
    }

    private static Task<SetupStep> GetCurrentSetupStep([FromServices] IMediator mediator)
    {
        var query = new GetCurrentSetupStepQuery();
        return mediator.Send(query);
    }

    private static Task<DockLightEnvironmentConfig> GetDockLightProtocols([FromServices] IMediator mediator)
    {
        var query = new GetPossibleDockerProtocolsQuery();
        return mediator.Send(query);
    }

    private static Task StartSetup([FromBody] StartSetupCommand startSetupCommand, [FromServices] IMediator mediator)
    {
        //_ = mediator.Send(startSetupCommand);//sets current step to "Welcome"
        return mediator.Send(startSetupCommand);
    }
    private static Task SetSetupStarted([FromBody] SetupCommand<SetupStartedCommand> setupStartedCommand, [FromServices] IMediator mediator)
    {
        return mediator.Send(setupStartedCommand);
    }

    private static Task UpdateStep([FromBody] SetupCommand updateSetupStepCommand, [FromServices] IMediator mediator)
    {
        return mediator.Send(updateSetupStepCommand);
    }

    private static Task CreateUserAccount([FromBody] SetupCommand<CreateFirstUserAccountCommand> createUserAccountCommand, [FromServices] IMediator mediator)
    {
        //_ = mediator.Send(createUserAccountCommand);//sets curent step to CreateNewUserAccount
        return mediator.Send(createUserAccountCommand);
    }

    private static Task CreateDockLightEnvironment([FromBody] SetupCommand<CreateDockLightEnvironmentCommand> createDockLightEnvironmentCommand, [FromServices] IMediator mediator)
    {
        _ = mediator.Send(createDockLightEnvironmentCommand);//sets current step to "ConnectToDocker"
        return mediator.Send(createDockLightEnvironmentCommand.InternalCommand);
    }

    private static Task FinishSetup([FromBody] SetupCommand<FinishSetupCommand> finishSetupCommand, [FromServices] IMediator mediator)
    {
        _ = mediator.Send(finishSetupCommand);//Sets current step to finished and sets setup to finished
        return mediator.Send(finishSetupCommand.InternalCommand);
    }
}
