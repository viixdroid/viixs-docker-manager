using ViixsDockerManager.Shared.Database.Entities;
using ViixsDockerManager.Shared.Repositories;

namespace ViixsDockerManager.Shared.Database.Repositories;

public interface IDatabaseWriteRepository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
{
    Task Save(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(TEntity entity);
}
