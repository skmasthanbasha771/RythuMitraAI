using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RythuMitraAI.Domain.Entities;

namespace RythuMitraAI.Infrastructure.Persistence.Configurations;

/// <summary>
/// EF Core configuration for the <see cref="Disease"/> entity.
/// Configures table mapping, property constraints, indexes and auditing fields.
/// </summary>
public sealed class DiseaseConfiguration : IEntityTypeConfiguration<Disease>
{
    /// <summary>
    /// Configures the <see cref="Disease"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<Disease> builder)
    {
        builder.ToTable("Diseases");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.DiseaseCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(d => d.DiseaseName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(d => d.CropType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.Symptoms)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(d => d.Causes)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(d => d.Treatment)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(d => d.Prevention)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(d => d.Severity)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(d => d.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Indexes
        builder.HasIndex(d => d.DiseaseCode).IsUnique();
        builder.HasIndex(d => d.DiseaseName);
        builder.HasIndex(d => d.CropType);

        // Auditing fields from AuditableEntity
        builder.Property(d => d.CreatedAt)
            .IsRequired();

        builder.Property(d => d.CreatedBy)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(d => d.ModifiedAt)
            .IsRequired(false);

        builder.Property(d => d.ModifiedBy)
            .HasMaxLength(100)
            .IsRequired(false);
    }
}
