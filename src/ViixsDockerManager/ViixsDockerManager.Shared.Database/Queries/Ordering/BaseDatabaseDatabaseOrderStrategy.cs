namespace ViixsDockerManager.Shared.Database.Queries.Ordering;

public abstract class BaseDatabaseDatabaseOrderStrategy<TEntity> : IDatabaseOrderStrategy<TEntity>
{
    IQueryable<TEntity> IDatabaseOrderStrategy<TEntity>.ApplyOrdering(IQueryable<TEntity> query, bool isFirstOrder)
        => ApplyOrdering(query, isFirstOrder);

    protected abstract IQueryable<TEntity> ApplyOrdering(IQueryable<TEntity> query, bool isFirstOrder);
}
