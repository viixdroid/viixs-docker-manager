using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.Mediator.Queries;
using ViixsDockerManager.Shared.Database.Queries.Filters;

namespace ViixsDockerManager.DockLight.Environments.Models.Queries;

public record GetAllDockLightEnvironmentsQuery(IDatabaseQueryFilter<DockLightEnvironment>? filter = null)
    : IQuery<IEnumerable<DockLightEnvironment>>
{

}
