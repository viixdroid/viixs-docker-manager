using ViixsDockerManager.Shared.Database.Queries.Filters;
using ViixsDockerManager.Shared.Database.Queries.Ordering;

namespace ViixsDockerManager.Shared.Database.Queries;

public interface IDatabaseQueryBuilder<TQueryResult> : IQuerySource<TQueryResult>
{
    /// <summary>
    /// This method applies filtering to the queries.
    /// </summary>
    /// <param name="filter">
    /// The filter to apply
    /// </param>
    /// <returns>
    /// The filtered query
    /// </returns>
    IDatabaseQueryBuilder<TQueryResult> ApplyFilter(IDatabaseQueryFilter<TQueryResult>? filter = null);

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
    IDatabaseQueryBuilder<TQueryResult> ApplyOrdering(IEnumerable<IDatabaseOrderStrategy<TQueryResult>>? orderStrategies);
}
