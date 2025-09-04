using System.Linq.Expressions;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.Shared.Database.Queries.Filters;

namespace ViixsDockerManager.DockLight.Shared.Queries.Filters;

public class DockLightEnvironmentByEnvironmentIdFilter(Guid environmentId) : IDatabaseQueryFilter<DockLightEnvironment>
{
    public Expression<Func<DockLightEnvironment, bool>> GetPredicate()
        => dle => dle.EnvironmentId == environmentId;
}
