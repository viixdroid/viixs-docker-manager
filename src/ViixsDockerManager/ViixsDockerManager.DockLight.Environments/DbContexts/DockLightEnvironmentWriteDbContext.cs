using Microsoft.EntityFrameworkCore;
using ViixsDockerManager.DockLight.Environments.Models.Configurations;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.Shared.Database.Contexts;

namespace ViixsDockerManager.DockLight.Environments.DbContexts;

public class DockLightEnvironmentWriteDbContext(DbContextOptions options) : BaseWriteDbContext(options)
{
    public DbSet<DockLightEnvironment> DockLightEnvironments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new DockLightEnvironmentEntityConfiguration());
        // modelBuilder.Entity<DockLightEnvironment>(docklightEnvironment =>
        // {
        //     docklightEnvironment.HasData(
        //         new DockLightEnvironment() { Id = 1, EnvironmentId = Guid.Parse("34805C86-9086-45E1-B264-241983ACC044"), Name = "Local", ApiLocation = Unix.Socket },
        //         new DockLightEnvironment() { Id = 2, EnvironmentId = Guid.Parse("65670BC8-FB6E-413A-9492-3BA24FD4F8FF"), Name = "LocalWindows", ApiLocation = DockLightConstants.Windows.Npipe },
        //         new DockLightEnvironment() { Id = 3, EnvironmentId = Guid.Parse("1ACF8E3C-495A-4258-A280-CC7ACF504C0E"), Name = "Nas", ApiLocation = "http://nas" },
        //         new DockLightEnvironment() { Id = 4, EnvironmentId = Guid.Parse("4D9E6256-D601-41F4-ABE6-A42F40100008"), Name = "Nuc", ApiLocation = "http://nuc" }
        //     );
        // });
    }
}
