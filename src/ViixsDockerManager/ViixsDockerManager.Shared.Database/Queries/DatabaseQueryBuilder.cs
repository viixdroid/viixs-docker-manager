using ViixsDockerManager.Shared.Database.Entities;
using ViixsDockerManager.Shared.Database.Exceptions;
using ViixsDockerManager.Shared.Database.Queries.Filters;
using ViixsDockerManager.Shared.Database.Queries.Ordering;

namespace ViixsDockerManager.Shared.Database.Queries;

/// <summary>
/// The class represents a query
/// </summary>
/// <param name="queryObject">
/// The IQueryable to filter
/// </param>
/// <typeparam name="TEntity">
/// The expected result of the query
/// </typeparam>
public sealed class DatabaseQueryBuilder<TEntity>(IQueryable<TEntity> queryObject)
    : IDatabaseQueryBuilder<TEntity>
    where TEntity : class, IEntity
{
    private IQueryable<TEntity> _query = queryObject ?? throw new QueryCannotBeNullException();

    /// <summary>
    /// This method applies filtering to the queries.
    /// </summary>
    /// <param name="filter">
    /// The filter to apply
    /// </param>
    /// <returns>
    /// The filtered query
    /// </returns>
    public IDatabaseQueryBuilder<TEntity> ApplyFilter(IQueryFilter<TEntity>? filter = null)
    {
        if (filter is null)
        {
            return this;
        }

        _query = _query.Where(filter.GetPredicate());
        return this;
    }

    /// <summary>
    /// Applies ordering to the query
    /// </summary>
    /// <param name="orderStrategies"></param>
    /// <returns>
    /// the ordered query
    /// </returns>
    /// <remarks>
    /// This method applies the filters as passed. If you have filter by name and a filter by id, and that is the order
    /// in which the filters are passed, the filters are executed as is.
    /// </remarks>
    public IDatabaseQueryBuilder<TEntity> ApplyOrdering(IEnumerable<IOrderStrategy<TEntity>>? orderStrategies)
    {
        if (orderStrategies is null)
        {
            return this;
        }

        var orderStrategyList = orderStrategies.ToList();
        if (orderStrategyList.Count == 0)
        {
            return this;
        }

        var isFirstOrderBy = false;
        foreach (var orderStrategy in orderStrategyList)
        {
            _query = orderStrategy.ApplyOrdering(_query, isFirstOrderBy);
            isFirstOrderBy = false;
        }

        return this;
    }

    IQueryable<TEntity> IQuerySource<TEntity>.AsQueryable() => _query;
}
