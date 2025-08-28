using System.Linq.Expressions;

namespace ViixsDockerManager.Shared.Database.Queries.Filters;

public interface IQueryFilter<TEntity>
{
    Expression<Func<TEntity, bool>> GetPredicate();
}
