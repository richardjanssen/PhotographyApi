using Business.Entities.Models;
using Business.Entities.Recipes;
using Common.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Database;
public class RiesjDbContext(DbContextOptions<RiesjDbContext> options, IDateTimeProvider dateTimeProvider) : DbContext(options)
{
    public DbSet<Recipe> Recipes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RiesjDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<EntityBase>() ?? [])
        {
            entry.Property(e => e.DateModifiedUtc).CurrentValue = dateTimeProvider.UtcNow;
            entry.Property(e => e.RowVersion).CurrentValue += 1;
        }
        return await base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        throw new InvalidOperationException("Always use SaveChangesAsync() instead of SaveChanges()");
    }
}
