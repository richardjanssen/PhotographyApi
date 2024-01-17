using Business.Components;
using Business.Components.Authentication;
using Business.Components.HighlightsTimeline;
using Business.Components.HighlightsTimeline.Internal;
using Business.Components.HikerUpdates;
using Business.Components.Internal;
using Business.Components.Locations;
using Business.Components.Locations.Internal;
using Business.Interfaces;
using Business.Interfaces.Authentication;
using Business.Interfaces.HighlightsTimeline;
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
        .AddScoped<IAuthenticateAccountQuery, AuthenticateAccountQuery>()
        .AddHighlightsTimelineBindings()
        .AddScoped<IAddManualLocationQuery, AddManualLocationQuery>()
        .AddScoped<IAddAutomaticLocationQuery, AddAutomaticLocationQuery>()
        .AddScoped<IAddSatelliteMessengerLocationQuery, AddSatelliteMessengerLocationQuery>()
        .AddScoped<IGetDistanceBetweenLocationsQuery,  GetDistanceBetweenLocationsQuery>()
        .AddScoped<IGetLocationsQuery, GetLocationsQuery>()
        .AddScoped<IDeleteLocationQuery, DeleteLocationQuery>()
        .AddScoped<IDeleteHikerUpdateQuery, DeleteHikerUpdateQuery>()
        ;

    private static IServiceCollection AddHighlightsTimelineBindings(this IServiceCollection services) => services
        .AddScoped<IGetHighlightsTimelineQuery, GetHighlightsTimelineQuery>()
        .AddScoped<IGetPointHighlightsQuery, GetPointHighlightsQuery>()
        .AddScoped<IGetTimelineHikerUpdatesQuery, GetTimelineHikerUpdatesQuery>()
        .AddScoped<IGetTimelineHikerLocationsQuery, GetTimelineHikerLocationsQuery>()
        ;
}
