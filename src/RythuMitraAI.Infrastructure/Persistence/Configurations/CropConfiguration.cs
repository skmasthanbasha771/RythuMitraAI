using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RythuMitraAI.Domain.Entities;

namespace RythuMitraAI.Infrastructure.Persistence.Configurations;

/// <summary>
/// EF Core configuration for the <see cref="Crop"/> entity.
/// Configures table mapping, property constraints, indexes and relationships.
/// </summary>
public sealed class CropConfiguration : IEntityTypeConfiguration<Crop>
{
    /// <summary>
    /// Configures the <see cref="Crop"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<Crop> builder)
    {
        builder.ToTable("Crops");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.CropCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.CropName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(c => c.CropCategory)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Season)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.SowingDate)
            .IsRequired();

        builder.Property(c => c.HarvestDate)
            .IsRequired(false);

        builder.Property(c => c.Area)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(c => c.AreaUnit)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.FarmerId)
            .IsRequired();

        builder.Property(c => c.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(c => c.CropCode).IsUnique();
        builder.HasIndex(c => c.FarmerId);
        builder.HasIndex(c => c.CropName);

        builder.HasOne<Farmer>()
            .WithMany()
            .HasForeignKey(c => c.FarmerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.Property(c => c.CreatedBy)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(c => c.ModifiedAt)
            .IsRequired(false);

        builder.Property(c => c.ModifiedBy)
            .HasMaxLength(100)
            .IsRequired(false);
    }
}
