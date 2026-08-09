using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RythuMitraAI.Domain.Entities;

namespace RythuMitraAI.Infrastructure.Persistence.Configurations;

/// <summary>
/// EF Core configuration for the <see cref="Fertilizer"/> entity.
/// </summary>
public sealed class FertilizerConfiguration : IEntityTypeConfiguration<Fertilizer>
{
    public void Configure(EntityTypeBuilder<Fertilizer> builder)
    {
        builder.ToTable("Fertilizers");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.FertilizerCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(f => f.FertilizerCode).IsUnique();

        builder.Property(f => f.FertilizerName)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasIndex(f => f.FertilizerName);

        builder.Property(f => f.Brand)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(f => f.FertilizerType)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(f => f.FertilizerType);

        builder.Property(f => f.Nitrogen).HasPrecision(5, 2);
        builder.Property(f => f.Phosphorus).HasPrecision(5, 2);
        builder.Property(f => f.Potassium).HasPrecision(5, 2);

        builder.Property(f => f.RecommendedCrop)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(f => f.RecommendedSoil)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(f => f.Description)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(f => f.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Auditing fields are inherited from AuditableEntity (CreatedAt, CreatedBy, ModifiedAt, ModifiedBy)
    }
}
