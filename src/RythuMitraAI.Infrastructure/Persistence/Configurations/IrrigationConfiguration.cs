using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RythuMitraAI.Domain.Entities;

namespace RythuMitraAI.Infrastructure.Persistence.Configurations;

/// <summary>
/// EF Core configuration for the <see cref="Irrigation"/> entity.
/// Configures table mapping, property constraints, indexes and relationships.
/// </summary>
public sealed class IrrigationConfiguration : IEntityTypeConfiguration<Irrigation>
{
    /// <summary>
    /// Configures the <see cref="Irrigation"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<Irrigation> builder)
    {
        builder.ToTable("Irrigations");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.IrrigationCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(i => i.FarmerId)
            .IsRequired();

        builder.Property(i => i.CropId)
            .IsRequired();

        builder.Property(i => i.IrrigationType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.WaterSource)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.IrrigationDate)
            .IsRequired();

        builder.Property(i => i.DurationInMinutes)
            .IsRequired();

        builder.Property(i => i.WaterQuantity)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(i => i.WaterUnit)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(i => i.Remarks)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(i => i.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Indexes
        builder.HasIndex(i => i.IrrigationCode).IsUnique();
        builder.HasIndex(i => i.FarmerId);
        builder.HasIndex(i => i.CropId);
        builder.HasIndex(i => i.IrrigationDate);

        // Relationships
        builder.HasOne<Farmer>()
            .WithMany()
            .HasForeignKey(i => i.FarmerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Crop>()
            .WithMany()
            .HasForeignKey(i => i.CropId)
            .OnDelete(DeleteBehavior.Restrict);

        // Auditing properties
        builder.Property(i => i.CreatedAt)
            .IsRequired();

        builder.Property(i => i.CreatedBy)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(i => i.ModifiedAt)
            .IsRequired(false);

        builder.Property(i => i.ModifiedBy)
            .HasMaxLength(100)
            .IsRequired(false);
    }
}
