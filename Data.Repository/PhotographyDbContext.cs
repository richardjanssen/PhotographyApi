using Data.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class PhotographyDbContext : DbContext
{
    public PhotographyDbContext(DbContextOptions<PhotographyDbContext> dbcontextoption) : base(dbcontextoption) { }

    public DbSet<Photo> Photos => Set<Photo>();
    public DbSet<Image> Images => Set<Image>();
    public DbSet<Account> Accounts => Set<Account>();
}
