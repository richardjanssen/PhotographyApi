using Business.Components;
using Business.Components.GetHighlights;
using Business.Components.Internal;
using Business.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Ioc;

public static class BusinessBindings
{
    public static IServiceCollection AddBusinessBindings(this IServiceCollection services) => services
        .AddScoped<IGetPhotosByDateDescendingQuery, GetPhotosByDateDescendingQuery>()
        .AddScoped<ISaveImageToFolderQuery, SaveImageToFolderQuery>()
        .AddScoped<IAddPhotoQuery, AddPhotoQuery>()
        .AddScoped<IAuthenticationComponent, AuthenticationComponent>()
        .AddScoped<IGetHighlightsQuery, GetHighlightsQuery>()
        ;

}
