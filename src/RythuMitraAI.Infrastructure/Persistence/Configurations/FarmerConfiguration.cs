using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RythuMitraAI.Domain.Entities;

namespace RythuMitraAI.Infrastructure.Persistence.Configurations;

/// <summary>
/// EF Core configuration for the <see cref="Farmer"/> entity.
/// Configures table name, keys, indexes and property constraints using the Fluent API.
/// </summary>
public sealed class FarmerConfiguration : IEntityTypeConfiguration<Farmer>
{
    /// <summary>
    /// Configures the <see cref="Farmer"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<Farmer> builder)
    {
        // Table
        builder.ToTable("Farmers");

        // Primary Key
        builder.HasKey(f => f.Id);

        // Farmer code
        builder.Property(f => f.FarmerCode)
            .IsRequired()
            .HasMaxLength(50);
        builder.HasIndex(f => f.FarmerCode).IsUnique();

        // Names
        builder.Property(f => f.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(f => f.LastName)
            .IsRequired()
            .HasMaxLength(100);

        // Contact
        builder.Property(f => f.PhoneNumber)
            .HasMaxLength(20)
            .IsRequired(false);
        builder.HasIndex(f => f.PhoneNumber);

        builder.Property(f => f.Email)
            .IsRequired()
            .HasMaxLength(255);

        // Demographics
        builder.Property(f => f.DateOfBirth)
            .IsRequired(false);

        builder.Property(f => f.Gender)
            .HasMaxLength(50)
            .IsRequired(false);

        // Address
        builder.Property(f => f.Address)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(f => f.Village)
            .HasMaxLength(150)
            .IsRequired(false);

        builder.Property(f => f.Mandal)
            .HasMaxLength(150)
            .IsRequired(false);

        builder.Property(f => f.District)
            .HasMaxLength(150)
            .IsRequired(false);
        builder.HasIndex(f => f.District);

        builder.Property(f => f.State)
            .HasMaxLength(150)
            .IsRequired(false);

        builder.Property(f => f.Pincode)
            .HasMaxLength(20)
            .IsRequired(false);

        // Land
        builder.Property(f => f.LandArea)
            .HasPrecision(18, 2)
            .IsRequired(false);

        builder.Property(f => f.LandUnit)
            .HasMaxLength(50)
            .IsRequired(false);

        // Profile image
        builder.Property(f => f.ProfileImageUrl)
            .HasMaxLength(2048)
            .IsRequired(false);

        // Status
        builder.Property(f => f.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // NOTE: Auditing properties (CreatedAt/ModifiedAt) are declared on AuditableEntity and handled elsewhere.
    }
}
