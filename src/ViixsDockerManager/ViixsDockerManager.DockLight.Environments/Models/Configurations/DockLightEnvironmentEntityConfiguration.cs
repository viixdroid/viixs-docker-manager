using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViixsDockerManager.DockLight.Shared.Entities;

namespace ViixsDockerManager.DockLight.Environments.Models.Configurations;

public class DockLightEnvironmentEntityConfiguration : IEntityTypeConfiguration<DockLightEnvironment>
{
    public void Configure(EntityTypeBuilder<DockLightEnvironment> builder)
    {
        builder.ToTable("DockLightEnvironments");
        builder.HasKey(dle => dle.Id);
        builder.HasAlternateKey(dle => dle.EnvironmentId);
        builder.Property(dle => dle.EnvironmentId).ValueGeneratedOnAdd();
        builder.Property(dle => dle.Id).ValueGeneratedOnAdd();
    }
}
