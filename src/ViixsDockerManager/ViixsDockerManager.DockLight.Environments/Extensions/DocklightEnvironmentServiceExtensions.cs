using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ViixsDockerManager.DockLight.Environments.Entities;
using ViixsDockerManager.Shared.Database.Extensions;
using ViixsDockerManager.Shared.Database.Sqlite.Extensions;

namespace ViixsDockerManager.DockLight.Environments.Extensions;

public static class DocklightEnvironmentServiceExtensions
{
    public static IServiceCollection AddDocklightEnvironmentServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddReadDatabaseServices<DocklightEnvironmentReadDbContext>(configuration);
        services.AddEntityServices<DocklightEnvironmentReadDbContext, DocklightEnvironment>();
        return services;
    }

    public static Task RunDocklightEnvironmentMigrations(this IHost serviceHost)
    {
        return serviceHost.RunMigrations<DocklightEnvironmentReadDbContext>();
    }
}
