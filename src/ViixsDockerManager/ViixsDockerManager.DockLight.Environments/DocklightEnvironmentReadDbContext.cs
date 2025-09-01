using Microsoft.EntityFrameworkCore;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.Shared.Database.Contexts;

namespace ViixsDockerManager.DockLight.Environments;

public class DocklightEnvironmentReadDbContext(DbContextOptions options) : BaseReadDbContext(options)
{
    public IQueryable<DocklightEnvironment> DocklightEnvironments => Set<DocklightEnvironment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<DocklightEnvironment>(docklightEnvironment =>
        {
            docklightEnvironment.HasKey(dle => dle.Id);
            docklightEnvironment.HasAlternateKey(dle => dle.EnvironmentId);
            docklightEnvironment.Property(dle => dle.EnvironmentId).ValueGeneratedOnAdd();
            docklightEnvironment.Property(dle => dle.Id).ValueGeneratedOnAdd();
            //TODO: Remove when added write functionality
            docklightEnvironment.HasData(
                new DocklightEnvironment() { Id = 1, EnvironmentId = Guid.Parse("34805C86-9086-45E1-B264-241983ACC044"), Name = "Local", ApiLocation = "/var/docker/docker.sock" },
                new DocklightEnvironment() { Id = 2, EnvironmentId = Guid.Parse("1ACF8E3C-495A-4258-A280-CC7ACF504C0E"), Name = "Nas", ApiLocation = "http://nas" },
                new DocklightEnvironment() { Id = 3, EnvironmentId = Guid.Parse("4D9E6256-D601-41F4-ABE6-A42F40100008"), Name = "Nuc", ApiLocation = "http://nuc" }
            );
        });
    }
}
