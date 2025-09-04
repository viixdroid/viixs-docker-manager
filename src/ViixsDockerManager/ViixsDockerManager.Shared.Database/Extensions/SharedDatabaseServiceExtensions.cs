using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ViixsDockerManager.Shared.Database.Contexts;
using ViixsDockerManager.Shared.Database.Entities;
using ViixsDockerManager.Shared.Database.Repositories;
using ViixsDockerManager.Shared.Database.UnitOfWorks;

namespace ViixsDockerManager.Shared.Database.Extensions;

public static class SharedDatabaseServiceExtensions
{
    public static IServiceCollection AddReadEntityServices<TReadDbContext, TEntity>(this IServiceCollection services)
        where TReadDbContext : DbContext, IReadDbContext
        where TEntity : class, IEntity
    {
        services
            .AddScoped<IReadUnitOfWork<TEntity>, ReadUnitOfWork<TEntity>>()
            .AddScoped<IReadRepository<TEntity>, ReadRepository<TReadDbContext, TEntity>>();
        return services;
    }

    public static IServiceCollection AddWriteEntityServices<TWriteDbContext, TEntity>(this IServiceCollection services)
        where TWriteDbContext : DbContext, IWriteDbContext
        where TEntity : class, IEntity
    {
        services
            .AddScoped<IDatabaseWriteRepository<TEntity>, DatabaseWriteRepository<TWriteDbContext, TEntity>>();
        return services;
    }
}
