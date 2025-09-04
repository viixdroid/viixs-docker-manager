using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.DockLight.Shared.Models.Commands;
using ViixsDockerManager.DockLight.Shared.Models.Queries;
using ViixsDockerManager.Mediator.Queries;
using ViixsDockerManager.Shared.Database.Repositories;

namespace ViixsDockerManager.DockLight.Shared.Handlers;

public class GetAllDockLightEnvironmentsHandler(IReadRepository<DockLightEnvironment> readRepository)
    : IQueryHandler<GetAllDockLightEnvironmentsQuery, IEnumerable<DockLightEnvironment>>
{
    public Task<IEnumerable<DockLightEnvironment>> Execute(GetAllDockLightEnvironmentsQuery query, CancellationToken cancellationToken = default)
    {
        return readRepository.GetAllAsync();
    }

}
