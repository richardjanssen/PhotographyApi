using Business.Components;
using Business.Components.GetHighlights;
using Business.Components.Internal;
using Business.Components.Locations;
using Business.Interfaces;
using Business.Interfaces.GetHighlights;
using Business.Interfaces.Locations;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Ioc;

public static class BusinessBindings
{
    public static IServiceCollection AddBusinessBindings(this IServiceCollection services) => services
        .AddScoped<IGetPhotosByDateDescendingQuery, GetPhotosByDateDescendingQuery>()
        .AddScoped<IGetHikerUpdateDetailsQuery, GetHikerUpdateDetailsQuery>()
        .AddScoped<ISaveImageToFolderQuery, SaveImageToFolderQuery>()
        .AddScoped<IAddPhotoQuery, AddPhotoQuery>()
        .AddScoped<IAuthenticationComponent, AuthenticationComponent>()
        .AddScoped<IGetHighlightsQuery, GetHighlightsQuery>()
        .AddScoped<IAddManualLocationQuery, AddManualLocationQuery>()
        ;

}
