using ViixsDockerManager.Shared.Database.Queries;

namespace ViixsDockerManager.Shared.Database.UnitOfWorks;

public interface IReadUnitOfWork<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllForQueryAsync(IQuerySource<TEntity> query);
    Task<TEntity?> GetFirstOrDefaultForQueryAsync(IQuerySource<TEntity> query);
    Task<TEntity?> GetSingleOrDefaultForQueryAsync(IQuerySource<TEntity> query);
    Task<TEntity> GetSingleForQueryAsync(IQuerySource<TEntity> query);
    Task<int> GetCountForQueryAsync(IQuerySource<TEntity> query);
    Task<bool> AnyAsync(IQuerySource<TEntity> query);
}
