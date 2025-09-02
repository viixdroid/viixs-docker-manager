using System.Linq.Expressions;

namespace ViixsDockerManager.Shared.Database.Queries.Filters;

public interface IDatabaseQueryFilter<TEntity>
{
    Expression<Func<TEntity, bool>> GetPredicate();
}
