using Data.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class PhotographyContext : DbContext
{
    public PhotographyContext(DbContextOptions<PhotographyContext> dbcontextoption) : base(dbcontextoption) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => 
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Photography;Trusted_Connection=True;");

    public DbSet<Photo> Photos => Set<Photo>();
    public DbSet<Image> Images => Set<Image>();
}
