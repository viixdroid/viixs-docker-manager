using System.Reflection;
using ViixsDockerManager.Mediator.Queries;
using ViixsDockerManager.Shared.Attributes;
using ViixsDockerManager.Version.Queries;

namespace ViixsDockerManager.Version.Handlers;

[ViixsController(typeof(VersionObjQuery), Action = "obj", ControllerName = "Version")]
internal sealed class VersionObjRequestHandler : IQueryHandler<VersionObjQuery, Models.Version>
{
    public Task<Models.Version> Execute(VersionObjQuery query, CancellationToken cancellationToken = default)
    {
        var versionAttr = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>();
        return Task.FromResult(new Models.Version
        (
            versionAttr?.InformationalVersion ?? "unknown",
            Environment.GetEnvironmentVariable("GIT_COMMIT") ?? "unknown",
            Environment.GetEnvironmentVariable("BUILD_DATE") ?? "unknown"
        ));
    }
}
