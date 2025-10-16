using Microsoft.EntityFrameworkCore;
using ViixsDockerManager.Setup.Models.Entities;
using ViixsDockerManager.Setup.Models.Entities.Configurations;
using ViixsDockerManager.Shared.Database.Contexts;

namespace ViixsDockerManager.Setup.DbContexts;

internal sealed class SetupWriteDbContext(DbContextOptions options) : BaseWriteDbContext(options)
{
    public DbSet<SetupState> SetupStates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SetupStateConfiguration());
    }
}
