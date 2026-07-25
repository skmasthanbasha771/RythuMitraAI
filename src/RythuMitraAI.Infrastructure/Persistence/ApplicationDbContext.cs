using Microsoft.EntityFrameworkCore;
using RythuMitraAI.Domain.Common;

namespace RythuMitraAI.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Example: expose DbSets for derived entity types in your Infrastructure layer
    // public DbSet<YourEntity> YourEntities { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Optionally handle auditing here
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Configure shared mappings or apply configurations from assembly
    }
}
