using Microsoft.EntityFrameworkCore;
using ViixsDockerManager.Setup.Models.Entities;
using ViixsDockerManager.Setup.Models.Entities.Configurations;
using ViixsDockerManager.Shared.Database.Contexts;

namespace ViixsDockerManager.Setup.DbContexts;

internal sealed class SetupReadDbContext(DbContextOptions options) : BaseReadDbContext(options)
{
    public IQueryable<SetupState> SetupStates => Set<SetupState>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SetupStateConfiguration());
    }
}
