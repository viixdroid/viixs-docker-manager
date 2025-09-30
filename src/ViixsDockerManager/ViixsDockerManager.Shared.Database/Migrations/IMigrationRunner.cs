using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ViixsDockerManager.Shared.Database.Contexts;

namespace ViixsDockerManager.Shared.Database.Migrations;

public interface IMigrationRunner
{
    void AddContext<TContext>() where TContext : DbContext, IWriteDbContext;
    void AddContext(Type dbContextType);
    void RunMigrations(IHost serviceHost);
}
