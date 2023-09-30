using Data.Interfaces;
using Data.Repository;
using Data.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Ioc;

public static class DataBindings
{
    public static IServiceCollection AddDataBindings(this IServiceCollection services) => services
        .AddScoped<IPhotographyRepository, PhotographyJsonRepository>()
        .AddScoped<IPlacesRepository, PlacesRepository>()
        .AddScoped<IPhotographyManager, PhotographyJsonManager>();
}