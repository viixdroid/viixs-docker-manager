using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ViixsDockerManager.Shared.Database.Entities;
using ViixsDockerManager.Shared.Database.Queries;
using ViixsDockerManager.Shared.Database.Queries.Filters;
using ViixsDockerManager.Shared.Database.UnitOfWorks;

namespace ViixsDockerManager.Shared.Database.Repositories;

public class ReadRepository<TReadDbContext, TEntity>(
    IDbContextFactory<TReadDbContext> dbContextFactory,
    IReadUnitOfWork<TEntity> readUnitOfWork)
    : BaseReadRepository<TReadDbContext, TEntity>(dbContextFactory, readUnitOfWork), IReadRepository<TEntity>
    where TReadDbContext : DbContext
    where TEntity : class, IEntity
{
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var queryBuilder = new DatabaseQueryBuilder<TEntity>(GetQueryable());
        return await ReadUnitOfWork.GetAllForQueryAsync(queryBuilder);
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        var queryBuilder = new DatabaseQueryBuilder<TEntity>(GetQueryable());
        queryBuilder.ApplyFilter(new EntityByIdFilter<TEntity>(id));
        return await ReadUnitOfWork.GetSingleOrDefaultForQueryAsync(queryBuilder);
    }

    public async Task<TEntity?> GetByFilterAsync(IQueryFilter<TEntity> filter)
    {
        var queryBuilder = new DatabaseQueryBuilder<TEntity>(GetQueryable());
        queryBuilder.ApplyFilter(filter);
        return await ReadUnitOfWork.GetSingleOrDefaultForQueryAsync(queryBuilder);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(IQueryFilter<TEntity> filter)
    {
        var queryBuilder = new DatabaseQueryBuilder<TEntity>(GetQueryable());
        queryBuilder.ApplyFilter(filter);
        return await ReadUnitOfWork.GetAllForQueryAsync(queryBuilder);
    }

    public async Task<int> CountAsync(IQueryFilter<TEntity>? filter = null)
    {
        var queryBuilder = new DatabaseQueryBuilder<TEntity>(GetQueryable());
        if (filter != null)
        {
            queryBuilder.ApplyFilter(filter);
        }
        return await ReadUnitOfWork.GetCountForQueryAsync(queryBuilder);
    }
}
