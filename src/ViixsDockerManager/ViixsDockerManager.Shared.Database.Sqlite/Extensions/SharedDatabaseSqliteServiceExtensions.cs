using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ViixsDockerManager.Shared.Database.Contexts;

namespace ViixsDockerManager.Shared.Database.Sqlite.Extensions;

public static class SharedDatabaseSqliteServiceExtensions
{
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
        IConfiguration configuration)
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

            options.UseSqlite(builder.ToString());
#if DEBUG
            options.LogTo(Console.WriteLine);
#endif
        });
        return services;
    }

    public static async Task RunMigrations<TWriteDbContext>(this IHost serviceHost)
        where TWriteDbContext : DbContext, IWriteDbContext
    {
        using var scope = serviceHost.Services.CreateScope();
        var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<TWriteDbContext>>();
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        await dbContext.Database.MigrateAsync();
    }
}
