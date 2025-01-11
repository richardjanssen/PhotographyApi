using Business.Entities.Recipes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Database;
public class RiesjDbContext(DbContextOptions<RiesjDbContext> options) : DbContext(options)
{
    public DbSet<Recipe> Recipes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RiesjDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
