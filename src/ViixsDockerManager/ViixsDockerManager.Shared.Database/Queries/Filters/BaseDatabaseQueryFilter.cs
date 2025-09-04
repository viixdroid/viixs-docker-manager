using System.Linq.Expressions;

namespace ViixsDockerManager.Shared.Database.Queries.Filters;

/// <summary>
/// A base class for a query filter.
/// </summary>
public abstract class BaseDatabaseQueryFilter<TEntity> : IDatabaseQueryFilter<TEntity>
{
    public Expression<Func<TEntity, bool>> GetPredicate() => GetPredicateExpression();
    protected abstract Expression<Func<TEntity, bool>> GetPredicateExpression();
}
