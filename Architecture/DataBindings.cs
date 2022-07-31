using Data.Repository;
using Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Ioc;

public static class DataBindings
{

    public static IServiceCollection AddPhotographyDatabase(this IServiceCollection services, IConfiguration configuration) => services
        .AddDbContext<PhotographyDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("PhotographyDatabase")));

    public static IServiceCollection AddDataBindings(this IServiceCollection services) => services
        .AddScoped<IPhotographyRepository, PhotographyRepository>();
}