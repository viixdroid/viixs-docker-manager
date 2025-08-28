using Microsoft.EntityFrameworkCore;
using ViixsDockerManager.DockLight.Environments.Entities;
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
            docklightEnvironment.Property(dle => dle.Id).ValueGeneratedOnAdd();
            //TODO: Remove when added write functionality
            docklightEnvironment.HasData(
                new DocklightEnvironment() { Id = 1, Name = "Local", ApiLocation = "/var/docker/docker.sock" },
                new DocklightEnvironment() { Id = 2, Name = "Nas", ApiLocation = "http://nas" },
                new DocklightEnvironment() { Id = 3, Name = "Nuc", ApiLocation = "http://nuc" }
            );
        });
    }
}
