using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Data.Repository.Database;
using Common.Common;
using Microsoft.Extensions.Configuration;

namespace Data.Repository.Design;
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<RiesjDbContext>
{
    public RiesjDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddUserSecrets("e7b392b5-ece7-4d4a-872b-51e92c9128d2")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<RiesjDbContext>();
        optionsBuilder.UseSqlite(configuration["ConnectionStrings:RiesjDatabase"]);

        return new RiesjDbContext(optionsBuilder.Options, new DateTimeProvider());
    }
}