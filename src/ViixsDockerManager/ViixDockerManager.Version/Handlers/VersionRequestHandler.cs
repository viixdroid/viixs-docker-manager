using System.Reflection;
using Microsoft.AspNetCore.Routing;
using ViixDockerManager.Version.Queries;
using ViixsDockerManager.Mediator.Queries;
//using ViixDockerManager.Shared.Attributes;

namespace ViixDockerManager.Version.Handlers;

//[ViixController]
[ViixsDockerManager.Mediator.ConTest<VersionRequestQuery>(Action = "/version", ControllerName = "Version")]
internal sealed class VersionRequestHandler : IQueryHandler<VersionRequestQuery, string>
{
    public Task<string> Execute(VersionRequestQuery query, CancellationToken cancellationToken = default)
    {
        //TODO: use actual object to return version info
        //[..amountToSubstring]
        return Task.FromResult(Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            ?.InformationalVersion ?? "Unknown Version");
    }

    //GenCode
    public static class VersionRouteActions
    {
        public static RouteGroupBuilder MapSetupRouteActions(this RouteGroupBuilder builder)
        {
            builder.MapGet("/version", GetVersion);
            return builder;
        }

        public static Task<string> GetVersion([FromServices] IMediator mediator)
        {
            var query = new VersionRequestQuery();
            return mediator.Send(query);
        }
    }
}
