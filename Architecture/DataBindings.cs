using Data.Interfaces;
using Data.Proxies.GarminExploreMapShare;
using Data.Repository;
using Data.Repository.Database;
using Data.Repository.Interfaces;
using Data.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Ioc;

public static class DataBindings
{
    public static IServiceCollection AddDataBindings(this IServiceCollection services) => services
        .AddDbContextFactory<RiesjDbContext>(options => options.UseSqlite("name=ConnectionStrings:RiesjDatabase"))
        .AddTransient<IRecipeRepository, RecipeRepository>()
        .AddScoped<IPhotographyRepository, PhotographyJsonRepository>()
        .AddScoped<IPlacesRepository, PlacesRepository>()
        .AddScoped<ISettingsRepository, SettingsRepository>()
        .AddScoped<ITrailRepository, TrailRepository>()
        .AddScoped<IPhotographyManager, PhotographyJsonManager>()
        .AddProxies();

    private static IServiceCollection AddProxies(this IServiceCollection services) => services
        .AddScoped<IGarminExploreMapShareManager, GarminExploreMapShareManager>();
}