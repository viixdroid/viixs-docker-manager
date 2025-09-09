using Microsoft.EntityFrameworkCore;
using ViixsDockerManager.Shared.Database.Contexts;
using ViixsDockerManager.Shared.Database.Entities;
using ViixsDockerManager.Shared.Database.UnitOfWorks;

namespace ViixsDockerManager.Shared.Database.Repositories;

public abstract class BaseReadRepository<TReadDbContext, TEntity>(
    IDbContextFactory<TReadDbContext> dbContextFactory,
    IReadUnitOfWork<TEntity> readUnitOfWork)
    where TReadDbContext : DbContext, IReadDbContext
    where TEntity : class, IEntity
{
    private readonly TReadDbContext _context = dbContextFactory.CreateDbContext();

    protected IReadUnitOfWork<TEntity> ReadUnitOfWork { get; } = readUnitOfWork;

    protected IQueryable<TEntity> GetQueryable()
    {
        return _context.Set<TEntity>();
    }
}
