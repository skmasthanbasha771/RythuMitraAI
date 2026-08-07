using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RythuMitraAI.Domain.Entities;

namespace RythuMitraAI.Infrastructure.Persistence.Configurations;

/// <summary>
/// EF Core configuration for the <see cref="Weather"/> entity.
/// </summary>
public sealed class WeatherConfiguration : IEntityTypeConfiguration<Weather>
{
    public void Configure(EntityTypeBuilder<Weather> builder)
    {
        builder.ToTable("Weathers");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.WeatherCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(w => w.WeatherCode).IsUnique();

        builder.Property(w => w.FarmerId).IsRequired();
        builder.HasIndex(w => w.FarmerId);

        builder.Property(w => w.WeatherDate).IsRequired();
        builder.HasIndex(w => w.WeatherDate);

        builder.Property(w => w.Temperature).HasPrecision(5, 2);
        builder.Property(w => w.Humidity).HasPrecision(5, 2);
        builder.Property(w => w.Rainfall).HasPrecision(8, 2);
        builder.Property(w => w.WindSpeed).HasPrecision(5, 2);

        builder.Property(w => w.WeatherCondition)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(w => w.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Configure explicit navigation and foreign key to avoid shadow FK (FarmerId1)
        builder.HasOne(w => w.Farmer)
            .WithMany(f => f.Weathers)
            .HasForeignKey(w => w.FarmerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
