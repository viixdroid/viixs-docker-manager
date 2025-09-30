using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ViixsDockerManager.Shared.Database.Contexts;
using ViixsDockerManager.Shared.Exceptions;

namespace ViixsDockerManager.Shared.Database.Migrations;

public sealed class MigrationRunner : IMigrationRunner
{
    private readonly ICollection<Type> _migrationContextFactories = [];

    public void AddContext(Type dbContextType)
    {
        if (dbContextType.IsAssignableFrom(typeof(DbContext)))
        {
            throw new ViixsDockerManagerException($"Can only run migrations for types of DbContext. Type {dbContextType} is not a DbContext");
        }

        var factory = typeof(IDbContextFactory<>).MakeGenericType(dbContextType);
        _migrationContextFactories.Add(factory);
    }

    public void AddContext<TContext>()
        where TContext : DbContext, IWriteDbContext
    {
        var contextType = typeof(TContext);
        AddContext(contextType);
    }

    public void RunMigrations(IHost serviceHost)
    {
        using var scope = serviceHost.Services.CreateScope();
        foreach (var factory in _migrationContextFactories)
        {
            var dbContextFactory = scope.ServiceProvider.GetRequiredService(factory);

            var createDbContextMethod = factory.GetMethod(nameof(IDbContextFactory<DbContext>.CreateDbContext))!;

            using var dbContext = (DbContext)createDbContextMethod.Invoke(dbContextFactory, [])!;

            //We run each migration sync. We need to make sure the lock is available for each migrate.
            //Using MigrateAsync() will concurrently call the lock, causing a deadlock.
            dbContext.Database.Migrate();
        }
    }
}
