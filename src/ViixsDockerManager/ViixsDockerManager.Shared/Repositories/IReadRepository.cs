namespace ViixsDockerManager.Shared.Repositories;

public interface IReadRepository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();

}
