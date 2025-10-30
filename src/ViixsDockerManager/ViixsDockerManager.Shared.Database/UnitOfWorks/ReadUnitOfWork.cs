using Microsoft.EntityFrameworkCore;
using ViixsDockerManager.Shared.Database.Queries;

namespace ViixsDockerManager.Shared.Database.UnitOfWorks;

public class ReadUnitOfWork<TEntity> : IReadUnitOfWork<TEntity>
{
    public async Task<IEnumerable<TEntity>> GetAllForQueryAsync(IQuerySource<TEntity> query)
    {
        return await query.AsQueryable().ToListAsync();
    }

    public async Task<TEntity?> GetFirstOrDefaultForQueryAsync(IQuerySource<TEntity> query)
    {
        return await query.AsQueryable().FirstOrDefaultAsync();
    }

    public async Task<TEntity?> GetSingleOrDefaultForQueryAsync(IQuerySource<TEntity> query)
    {
        return await query.AsQueryable().SingleOrDefaultAsync();
    }

    public async Task<TEntity> GetSingleForQueryAsync(IQuerySource<TEntity> query)
    {
        return await query.AsQueryable().SingleAsync();
    }

    public async Task<int> GetCountForQueryAsync(IQuerySource<TEntity> query)
    {
        return await query.AsQueryable().CountAsync();
    }

    public async Task<bool> AnyAsync(IQuerySource<TEntity> query)
    {
        return await query.AsQueryable().AnyAsync();
    }
}
