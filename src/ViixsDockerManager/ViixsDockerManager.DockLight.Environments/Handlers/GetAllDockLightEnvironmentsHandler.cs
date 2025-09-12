using ViixsDockerManager.DockLight.Environments.Models.Queries;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.Mediator.Queries;
using ViixsDockerManager.Shared.Database.Repositories;

namespace ViixsDockerManager.DockLight.Environments.Handlers;

public class GetAllDockLightEnvironmentsHandler(IDatabaseReadRepository<DockLightEnvironment> readRepository)
    : IQueryHandler<GetAllDockLightEnvironmentsQuery, IEnumerable<DockLightEnvironment>>
{
    public Task<IEnumerable<DockLightEnvironment>> Execute(GetAllDockLightEnvironmentsQuery query, CancellationToken cancellationToken = default)
    {
        return readRepository.GetAllAsync();
    }

}
