using ViixsDockerManager.Shared.Database.Entities;
using ViixsDockerManager.Shared.Database.Queries.Filters;

namespace ViixsDockerManager.Shared.Database.Repositories;

public interface IReadRepository<TEntity> where TEntity : class, IEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
    Task<TEntity?> GetByFilterAsync(IQueryFilter<TEntity> filter);
    Task<IEnumerable<TEntity>> FindAsync(IQueryFilter<TEntity> filter);
    Task<int> CountAsync(IQueryFilter<TEntity>? filter = null);
}
