using System.Reflection;
using ViixsDockerManager.Mediator.Queries;
using ViixsDockerManager.Version.Queries;
using ViixsDockerManager.Shared.Attributes;

namespace ViixsDockerManager.Version.Handlers;

[ViixsController(ControllerName = "Version")]
internal sealed class VersionRequestHandler : IQueryHandler<VersionRequestQuery, string>
{
    public Task<string> Execute(VersionRequestQuery query, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            ?.InformationalVersion ?? "Unknown Version");
    }
}

//TODO: use actual object to return version info
//[..amountToSubstring]
