namespace ViixsDockerManager.Shared.Database.Queries.Ordering;

public interface IOrderStrategy<TEntity>
{
    /// <summary>
    /// Applies orderning on a given query
    /// </summary>
    /// <param name="query">the query to order</param>
    /// <param name="isFirstOrder">if true, use query.OrderBy, if false, use query.ThenBy</param>
    /// <returns>
    /// An filtered IQueryable
    /// </returns>
    IQueryable<TEntity> ApplyOrdering(IQueryable<TEntity> query, bool isFirstOrder);
}
