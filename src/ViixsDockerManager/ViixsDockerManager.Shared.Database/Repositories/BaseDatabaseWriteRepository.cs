using Microsoft.EntityFrameworkCore;
using ViixsDockerManager.Shared.Database.Contexts;
using ViixsDockerManager.Shared.Database.Entities;
using ViixsDockerManager.Shared.Database.UnitOfWorks;

namespace ViixsDockerManager.Shared.Database.Repositories;

public abstract class BaseDatabaseWriteRepository<TWriteDbContext, TEntity>(
    IDbContextFactory<TWriteDbContext> dbContextFactory
) : IDatabaseWriteRepository<TEntity>
    where TWriteDbContext : DbContext, IWriteDbContext
    where TEntity : class, IEntity
{
    private readonly TWriteDbContext _context = dbContextFactory.CreateDbContext();

    protected DbSet<TEntity> GetDbSet()
    {
        return _context.Set<TEntity>();
    }

    public virtual async Task Save(TEntity entity)
    {
        await GetDbSet().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task Update(TEntity entity)
    {
        GetDbSet().Update(entity);
        // var trackedEntity = _context.ChangeTracker.Entries<TEntity>().FirstOrDefault(entity => entity.Entity.Id == entityToUpdate.Id);
        // if (trackedEntity is null)
        // {
        //     _context.Attach(entityToUpdate);
        //     _context.Entry(entityToUpdate).State = EntityState.Modified;
        // }
        // else
        // {
        //     trackedEntity.CurrentValues.SetValues(entityToUpdate);
        // }

        await _context.SaveChangesAsync();
    }

    public virtual async Task Delete(TEntity entity)
    {
        GetDbSet().Remove(entity);
        await _context.SaveChangesAsync();
    }
}
