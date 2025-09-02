using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ViixsDockerManager.Shared.Database.Sqlite.Extensions;

public static class SharedDatabaseSqliteServiceExtensions
{
    public static IServiceCollection AddReadDatabaseServices<TReadDbContext>(this IServiceCollection services,
        IConfiguration configuration)
        where TReadDbContext : DbContext
    {
        services.AddPooledDbContextFactory<TReadDbContext>(options =>
        {
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            var connString = configuration.GetConnectionString("viixsdockermanager-db");
            options.UseSqlite(connString); //Only when in Aspire.
            options.LogTo(Console.WriteLine);
        });
        return services;
    }

    public static async Task RunMigrations<TReadDbContext>(this IHost serviceHost)
        where TReadDbContext : DbContext
    {
        using var scope = serviceHost.Services.CreateScope();
        var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<TReadDbContext>>();
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        await dbContext.Database.MigrateAsync();
    }
}
