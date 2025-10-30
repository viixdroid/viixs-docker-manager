using Microsoft.EntityFrameworkCore;
using ViixsDockerManager.Shared.Database.Contexts;
using ViixsDockerManager.Shared.Database.Entities;
using ViixsDockerManager.Shared.Database.Queries;
using ViixsDockerManager.Shared.Database.Queries.Filters;
using ViixsDockerManager.Shared.Database.UnitOfWorks;

namespace ViixsDockerManager.Shared.Database.Repositories;

public class DatabaseReadRepository<TReadDbContext, TEntity>(
    IDbContextFactory<TReadDbContext> dbContextFactory,
    IReadUnitOfWork<TEntity> readUnitOfWork)
    : BaseReadRepository<TReadDbContext, TEntity>(dbContextFactory, readUnitOfWork), IDatabaseReadRepository<TEntity>
    where TReadDbContext : DbContext, IReadDbContext
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

    public async Task<TEntity?> GetByFilterAsync(IDatabaseQueryFilter<TEntity> filter)
    {
        var queryBuilder = new DatabaseQueryBuilder<TEntity>(GetQueryable());
        queryBuilder.ApplyFilter(filter);
        return await ReadUnitOfWork.GetSingleOrDefaultForQueryAsync(queryBuilder);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(IDatabaseQueryFilter<TEntity> filter)
    {
        var queryBuilder = new DatabaseQueryBuilder<TEntity>(GetQueryable());
        queryBuilder.ApplyFilter(filter);
        return await ReadUnitOfWork.GetAllForQueryAsync(queryBuilder);
    }

    public async Task<int> CountAsync(IDatabaseQueryFilter<TEntity>? filter = null)
    {
        var queryBuilder = new DatabaseQueryBuilder<TEntity>(GetQueryable());
        if (filter != null)
        {
            queryBuilder.ApplyFilter(filter);
        }
        return await ReadUnitOfWork.GetCountForQueryAsync(queryBuilder);
    }

    public async Task<bool> AnyAsync(IDatabaseQueryFilter<TEntity>? filter = null)
    {
        var queryBuilder = new DatabaseQueryBuilder<TEntity>(GetQueryable());
        if (filter is not null)
        {
            queryBuilder.ApplyFilter(filter);
        }
        return await ReadUnitOfWork.AnyAsync(queryBuilder);
    }

    public async Task<TEntity?> GetFirstOrDefaultAsync(IDatabaseQueryFilter<TEntity>? filter = null)
    {
        var queryBuilder = new DatabaseQueryBuilder<TEntity>(GetQueryable());
        queryBuilder.ApplyFilter(filter);
        queryBuilder.ApplyOrdering([new TmpDefaultOrderStrategy<TEntity>()]);
        return await ReadUnitOfWork.GetFirstOrDefaultForQueryAsync(queryBuilder);
    }

    class TmpDefaultOrderStrategy<TEntity1> : Queries.Ordering.IDatabaseOrderStrategy<TEntity1>
            where TEntity1 : class, IEntity
    {
        public IQueryable<TEntity1> ApplyOrdering(IQueryable<TEntity1> query, bool isFirstOrder)
            => query.OrderBy(e => e.Id);
    }
}

