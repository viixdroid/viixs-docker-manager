using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ViixsDockerManager.Shared.Database.Contexts;
using ViixsDockerManager.Shared.Database.Models;
using ViixsDockerManager.Shared.Exceptions;

namespace ViixsDockerManager.Shared.Database.Sqlite.Extensions;

public static class SharedDatabaseSqliteServiceExtensions
{
    private static readonly ICollection<Type> _migrationContextFactories = new HashSet<Type>();

    public static IServiceCollection AddReadDatabaseServices<TReadDbContext>(this IServiceCollection services,
        IConfiguration configuration)
        where TReadDbContext : DbContext, IReadDbContext
    {
        services.AddPooledDbContextFactory<TReadDbContext>(options =>
        {
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            //TODO: move to helper or smth
            var connString = configuration.GetSection("ViixsDockerManager").GetValue<string>("viixsdockermanager-db");
            var builder = new SqliteConnectionStringBuilder()
            {
                DataSource = connString,
                Cache = SqliteCacheMode.Shared,
            };

            options.UseSqlite(builder.ToString());
#if DEBUG
            options.LogTo(Console.WriteLine);
#endif
        });
        return services;
    }

    public static IServiceCollection AddWriteDatabaseServices<TWriteDbContext>(this IServiceCollection services,
        IConfiguration configuration, Action<DbContextOptionsBuilder>? optionsBuilder = null, MigrationsHistory? migrationsHistory = null)
        where TWriteDbContext : DbContext, IWriteDbContext
    {
        services.AddPooledDbContextFactory<TWriteDbContext>(options =>
        {
            //TODO: Move to helper or something
            var connString = configuration.GetSection("ViixsDockerManager").GetValue<string>("viixsdockermanager-db");
            var builder = new SqliteConnectionStringBuilder()
            {
                DataSource = connString,
                Cache = SqliteCacheMode.Shared,
            };

            options.UseSqlite(builder.ToString(), sqliteDbContextOptions =>
            {
                if (migrationsHistory != null)
                {
                    sqliteDbContextOptions.MigrationsHistoryTable(migrationsHistory.GetMigrationTableName());
                }
                optionsBuilder?.Invoke(options);
            });

#if DEBUG
            options.LogTo(Console.WriteLine);
#endif

        });
        return services;
    }

    public static IServiceCollection AddDbContextPool<TDbContext>(this IServiceCollection services, IConfiguration configuration)
        where TDbContext : DbContext
    {
        var connString = configuration.GetSection("ViixsDockerManager").GetValue<string>("viixsdockermanager-db");
        var builder = new SqliteConnectionStringBuilder()
        {
            DataSource = connString,
            Cache = SqliteCacheMode.Shared,
        };

        services.AddDbContextPool<TDbContext>(options =>
        {
            options.UseSqlite(builder.ToString());

#if DEBUG
            options.LogTo(Console.WriteLine);
#endif
        });
        return services;
    }

    public static void AddContextForMigrationRunning<TContext>(this IHost serviceHost)
        where TContext : DbContext, IWriteDbContext
    {
        var contextType = typeof(TContext);
        var factory = typeof(IDbContextFactory<>).MakeGenericType(contextType);
        _migrationContextFactories.Add(factory);
    }

    public static void RunMigrations(this IHost serviceHost)
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
