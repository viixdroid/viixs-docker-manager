using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ViixsDockerManager.Shared.Database.Entities;
using ViixsDockerManager.Shared.Database.Repositories;
using ViixsDockerManager.Shared.Database.UnitOfWorks;

namespace ViixsDockerManager.Shared.Database.Extensions;

public static class SharedDatabaseServiceExtensions
{
    public static IServiceCollection AddEntityServices<TReadDbContext, TEntity>(this IServiceCollection services)
        where TReadDbContext : DbContext
        where TEntity : class, IEntity
    {
        services
            .AddScoped<IReadUnitOfWork<TEntity>, ReadUnitOfWork<TEntity>>()
            .AddScoped<IReadRepository<TEntity>, ReadRepository<TReadDbContext, TEntity>>();
        return services;
    }
}
