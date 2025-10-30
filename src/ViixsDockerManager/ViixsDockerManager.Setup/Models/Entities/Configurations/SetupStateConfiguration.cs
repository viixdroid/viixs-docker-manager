using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ViixsDockerManager.Setup.Models.Entities.Configurations;

internal sealed class SetupStateConfiguration : IEntityTypeConfiguration<SetupState>
{
    public void Configure(EntityTypeBuilder<SetupState> builder)
    {
        builder.ToTable(nameof(SetupState));
        builder.HasKey(setupStateEntity => setupStateEntity.Id);
        builder.HasAlternateKey(setupStateEntity => setupStateEntity.SetupId);
        builder.Property(setupStateEntity => setupStateEntity.Id).ValueGeneratedOnAdd();
    }
}
