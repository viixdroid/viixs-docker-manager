using ViixsDockerManager.Shared.Database.Entities;
using ViixsDockerManager.Shared.Database.Queries.Filters;
using ViixsDockerManager.Shared.Repositories;

namespace ViixsDockerManager.Shared.Database.Repositories;

public interface IDatabaseReadRepository<TEntity> : IReadRepository<TEntity>
    where TEntity : class, IEntity
{
    Task<TEntity?> GetByIdAsync(int id);
    Task<TEntity?> GetByFilterAsync(IDatabaseQueryFilter<TEntity> filter);
    Task<IEnumerable<TEntity>> FindAsync(IDatabaseQueryFilter<TEntity> filter);
    Task<int> CountAsync(IDatabaseQueryFilter<TEntity>? filter = null);
}
