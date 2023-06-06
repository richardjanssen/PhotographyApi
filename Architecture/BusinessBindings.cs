using Business.Components;
using Business.Components.GetHighlights;
using Business.Components.HikerUpdates;
using Business.Components.Internal;
using Business.Components.Locations;
using Business.Interfaces;
using Business.Interfaces.GetHighlights;
using Business.Interfaces.HikerUpdates;
using Business.Interfaces.Locations;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Ioc;

public static class BusinessBindings
{
    public static IServiceCollection AddBusinessBindings(this IServiceCollection services) => services
        .AddScoped<IGetPhotosByDateDescendingQuery, GetPhotosByDateDescendingQuery>()
        .AddScoped<IGetHikerUpdateDetailsQuery, GetHikerUpdateDetailsQuery>()
        .AddScoped<IGetHikerUpdatesQuery, GetHikerUpdatesQuery>()
        .AddScoped<ISaveImageToFolderQuery, SaveImageToFolderQuery>()
        .AddScoped<IAddPhotoQuery, AddPhotoQuery>()
        .AddScoped<IAuthenticationComponent, AuthenticationComponent>()
        .AddScoped<IGetHighlightsQuery, GetHighlightsQuery>()
        .AddScoped<IAddManualLocationQuery, AddManualLocationQuery>()
        .AddScoped<IGetLocationsQuery, GetLocationsQuery>()
        .AddScoped<IDeleteLocationQuery, DeleteLocationQuery>()
        ;
}
