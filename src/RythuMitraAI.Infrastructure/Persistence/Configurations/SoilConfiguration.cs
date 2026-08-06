using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RythuMitraAI.Domain.Entities;

namespace RythuMitraAI.Infrastructure.Persistence.Configurations;

/// <summary>
/// EF Core configuration for the <see cref="Soil"/> entity.
/// </summary>
public sealed class SoilConfiguration : IEntityTypeConfiguration<Soil>
{
    /// <summary>
    /// Configures the <see cref="Soil"/> entity.
    /// </summary>
    /// <param name="builder">Entity type builder.</param>
    public void Configure(EntityTypeBuilder<Soil> builder)
    {
        builder.ToTable("Soils");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.SoilCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(s => s.SoilCode).IsUnique();

        builder.Property(s => s.FarmerId).IsRequired();
        builder.HasIndex(s => s.FarmerId);

        builder.Property(s => s.PH).HasPrecision(5, 2);
        builder.Property(s => s.Moisture).HasPrecision(5, 2);
        builder.Property(s => s.Nitrogen).HasPrecision(8, 2);
        builder.Property(s => s.Phosphorus).HasPrecision(8, 2);
        builder.Property(s => s.Potassium).HasPrecision(8, 2);
        builder.Property(s => s.OrganicCarbon).HasPrecision(5, 2);

        builder.Property(s => s.TestDate).IsRequired();
        builder.HasIndex(s => s.TestDate);

        builder.Property(s => s.Remarks)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(s => s.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Relationship: one Farmer has many Soils, one Soil belongs to one Farmer
        builder.HasOne<Farmer>()
            .WithMany()
            .HasForeignKey(s => s.FarmerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
