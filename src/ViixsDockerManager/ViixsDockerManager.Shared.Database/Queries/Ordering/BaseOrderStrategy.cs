namespace ViixsDockerManager.Shared.Database.Queries.Ordering;

public abstract class BaseOrderStrategy<TEntity> : IOrderStrategy<TEntity>
{
    IQueryable<TEntity> IOrderStrategy<TEntity>.ApplyOrdering(IQueryable<TEntity> query, bool isFirstOrder)
        => ApplyOrdering(query, isFirstOrder);

    protected abstract IQueryable<TEntity> ApplyOrdering(IQueryable<TEntity> query, bool isFirstOrder);
}
