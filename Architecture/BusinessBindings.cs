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
        .AddTransient<IGetHikerUpdateDetailsQuery, GetHikerUpdateDetailsQuery>()
        .AddTransient<IGetHikerUpdatesQuery, GetHikerUpdatesQuery>()
        .AddTransient<ISaveImageToFolderQuery, SaveImageToFolderQuery>()
        .AddTransient<IAddPhotoQuery, AddPhotoQuery>()
        .AddTransient<IAuthenticateAccountQuery, AuthenticateAccountQuery>()
        .AddHighlightsTimelineBindings()
        .AddTransient<IAddManualLocationQuery, AddManualLocationQuery>()
        .AddTransient<IAddLocationByCoordinateAndDateQuery, AddLocationByCoordinateAndDateQuery>()
        .AddTransient<IAddSatelliteMessengerLocationQuery, AddSatelliteMessengerLocationQuery>()
        .AddTransient<IGetDistanceBetweenLocationsQuery,  GetDistanceBetweenLocationsQuery>()
        .AddTransient<IGetLocationsQuery, GetLocationsQuery>()
        .AddTransient<IGetMapLocationsQuery, GetMapLocationsQuery>()
        .AddTransient<IDeleteLocationQuery, DeleteLocationQuery>()
        .AddTransient<IDeleteHikerUpdateQuery, DeleteHikerUpdateQuery>()
        ;

    private static IServiceCollection AddHighlightsTimelineBindings(this IServiceCollection services) => services
        .AddTransient<IGetHighlightsTimelineQuery, GetHighlightsTimelineQuery>()
        .AddTransient<IGetPointHighlightsQuery, GetPointHighlightsQuery>()
        .AddTransient<IGetTimelineHikerUpdatesQuery, GetTimelineHikerUpdatesQuery>()
        .AddTransient<IGetTimelineHikerLocationsQuery, GetTimelineHikerLocationsQuery>()
        ;
}
