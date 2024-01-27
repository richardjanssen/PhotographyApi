using Data.Interfaces;
using Data.Proxies.GarminExploreMapShare;
using Data.Repository;
using Data.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Ioc;

public static class DataBindings
{
    public static IServiceCollection AddDataBindings(this IServiceCollection services) => services
        .AddScoped<IPhotographyRepository, PhotographyJsonRepository>()
        .AddScoped<IPlacesRepository, PlacesRepository>()
        .AddScoped<ISettingsRepository, SettingsRepository>()
        .AddScoped<ITrailRepository, TrailRepository>()
        .AddScoped<IPhotographyManager, PhotographyJsonManager>()
        .AddProxies();

    private static IServiceCollection AddProxies(this IServiceCollection services) => services
        .AddScoped<IGarminExploreMapShareManager, GarminExploreMapShareManager>();
}