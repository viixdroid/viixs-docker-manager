using ViixsDockerManager.DockLight.Models.Overview;
using ViixsDockerManager.DockLight.Models.Queries;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.Mediator.Queries;

namespace ViixsDockerManager.DockLight.Handlers;

internal class GetContainersForEnvironmentHandler(IDockerServiceFactory dockerServiceFactory)
    : DockLightHandlerBase(dockerServiceFactory), IQueryHandler<GetContainersForEnvironmentQuery, IEnumerable<ContainerSummary>>
{
    public async Task<IEnumerable<ContainerSummary>> Execute(GetContainersForEnvironmentQuery query, CancellationToken cancellationToken = default)
    {
        var service = await GetDockerContainerService(query.EnvironmentId, cancellationToken).ConfigureAwait(false);
        return await service.GetContainerListAsync().ConfigureAwait(false);
    }
}
