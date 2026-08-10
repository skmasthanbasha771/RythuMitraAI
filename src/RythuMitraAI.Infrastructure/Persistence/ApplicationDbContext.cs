using Microsoft.EntityFrameworkCore;
using RythuMitraAI.Domain.Common;
using RythuMitraAI.Domain.Entities;

namespace RythuMitraAI.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // DbSets for entity types
    public DbSet<User> Users => Set<User>();

    // Farmers
    public DbSet<Farmer> Farmers => Set<Farmer>();

    // Soils
    public DbSet<Soil> Soils => Set<Soil>();

    // Weathers
    public DbSet<Weather> Weathers => Set<Weather>();

    // Fertilizers
    public DbSet<Fertilizer> Fertilizers => Set<Fertilizer>();

    // Crops
    public DbSet<Crop> Crops => Set<Crop>();

    // Diseases
    public DbSet<Disease> Diseases => Set<Disease>();

    // Irrigations
    public DbSet<Irrigation> Irrigations => Set<Irrigation>();

    // Example: expose DbSets for other entity types in your Infrastructure layer
    // public DbSet<YourEntity> YourEntities => Set<YourEntity>();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Optionally handle auditing here
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all IEntityTypeConfiguration implementations from this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
