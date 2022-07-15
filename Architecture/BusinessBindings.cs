using Business.Components;
using Business.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Ioc;

public static class BusinessBindings
{
    public static IServiceCollection AddBusinessBindings(this IServiceCollection services) => services
        .AddScoped<IGetPhotosByDateDescendingQuery, GetPhotosByDateDescendingQuery>()
        .AddScoped<IAddPhotoQuery, AddPhotoQuery>();
}
