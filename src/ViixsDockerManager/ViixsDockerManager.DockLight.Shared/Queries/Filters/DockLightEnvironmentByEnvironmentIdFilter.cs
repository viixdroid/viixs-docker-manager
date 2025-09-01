using System.Linq.Expressions;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.Shared.Database.Queries.Filters;

namespace ViixsDockerManager.DockLight.Shared.Queries.Filters;

public class DockLightEnvironmentByEnvironmentIdFilter(Guid environmentId) : IQueryFilter<DocklightEnvironment>
{
    public Expression<Func<DocklightEnvironment, bool>> GetPredicate()
        => dle => dle.EnvironmentId == environmentId;
}
