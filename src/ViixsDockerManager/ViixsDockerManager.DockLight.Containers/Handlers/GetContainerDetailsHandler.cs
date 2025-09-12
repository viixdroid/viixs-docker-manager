using ViixsDockerManager.DockLight.Models.Details;
using ViixsDockerManager.DockLight.Models.Queries;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.Mediator.Queries;

namespace ViixsDockerManager.DockLight.Handlers;

internal class GetContainerDetailsHandler(IDockerServiceFactory dockerServiceFactory)
    : DockLightHandlerBase(dockerServiceFactory), IQueryHandler<GetContainerDetailsQuery, ContainerDetails>
{
    public async Task<ContainerDetails> Execute(GetContainerDetailsQuery query, CancellationToken cancellationToken = default)
    {
        var service = await GetDockerContainerService(query.EnvironmentId, cancellationToken).ConfigureAwait(false);
        return await service.GetContainerDetailAsync(query.ContainerId).ConfigureAwait(false);
    }
}
