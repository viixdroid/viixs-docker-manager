using Microsoft.EntityFrameworkCore;
using ViixsDockerManager.Shared.Database.Contexts;
using ViixsDockerManager.Shared.Database.Entities;

namespace ViixsDockerManager.Shared.Database.Repositories;

public class DatabaseWriteRepository<TWriteDbContext, TEntity>(IDbContextFactory<TWriteDbContext> contextFactory)
    : BaseDatabaseWriteRepository<TWriteDbContext, TEntity>(contextFactory)
    where TWriteDbContext : DbContext, IWriteDbContext
    where TEntity : class, IEntity
{
}
