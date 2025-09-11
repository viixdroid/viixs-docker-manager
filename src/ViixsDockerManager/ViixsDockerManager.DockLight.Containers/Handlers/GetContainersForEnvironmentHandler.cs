using ViixsDockerManager.DockLight.Models.Overview;
using ViixsDockerManager.DockLight.Models.Queries;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.Mediator.Queries;

namespace ViixsDockerManager.DockLight.Handlers;

internal class GetContainersForEnvironmentHandler(IDockerServiceRunner serviceRunner)
    : IQueryHandler<GetContainersForEnvironmentQuery, IEnumerable<ContainerSummary>>
{
    public async Task<IEnumerable<ContainerSummary>> Execute(GetContainersForEnvironmentQuery query, CancellationToken cancellationToken = default)
    {
        var service = await serviceRunner.GetServiceAsync<IDockerContainerService>(query.EnvironmentId).ConfigureAwait(false);

        return await service.GetContainerListAsync().ConfigureAwait(false);
    }
}
