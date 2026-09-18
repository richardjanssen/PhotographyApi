using Business.Entities.Groceries;
using Business.Entities.Models;
using Business.Entities.Recipes;
using Business.Entities.Users;
using Common.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using ZNetCS.AspNetCore.Logging.EntityFrameworkCore;

namespace Data.Repository.Database;

public class RiesjDbContext(DbContextOptions<RiesjDbContext> options, IDateTimeProvider dateTimeProvider) : DbContext(options)
{
    public DbSet<Log> Logs { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<GroceryListProduct> GroceryListProducts { get; set; }
    public DbSet<GroceryListRecurringProduct> GroceryListRecurringProducts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RiesjDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<EntityBase>() ?? [])
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Property(e => e.DateModifiedUtc).CurrentValue = dateTimeProvider.UtcNow;
                entry.Property(e => e.RowVersion).CurrentValue += 1;
            }

        }
        return await base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        if (ChangeTracker.Entries<EntityBase>().Any())
        {
            throw new InvalidOperationException("Always use SaveChangesAsync() instead of SaveChanges()");
        }
        return base.SaveChanges();
    }
}
