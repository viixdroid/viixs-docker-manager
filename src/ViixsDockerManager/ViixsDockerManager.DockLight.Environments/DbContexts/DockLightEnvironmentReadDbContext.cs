using Microsoft.EntityFrameworkCore;
using ViixsDockerManager.DockLight.Environments.Models.Configurations;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.Shared.Database.Contexts;

namespace ViixsDockerManager.DockLight.Environments.DbContexts;

public class DockLightEnvironmentReadDbContext(DbContextOptions options) : BaseReadDbContext(options)
{
    public IQueryable<DockLightEnvironment> DockLightEnvironments => Set<DockLightEnvironment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DockLightEnvironmentEntityConfiguration());
    }
}
