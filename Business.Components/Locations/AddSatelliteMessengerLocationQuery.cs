using Business.Components.Locations.Internal;
using Business.Entities;
using Business.Entities.Dto;
using Business.Interfaces.Locations;
using Data.Interfaces;
using Data.Proxies.GarminExploreMapShare;
using Microsoft.Extensions.Logging;

namespace Business.Components.Locations;
public class AddSatelliteMessengerLocationQuery(
    IPhotographyRepository photographyRepository,
    IGarminExploreMapShareManager garminExploreMapShareManager,
    ISettingsRepository settingsRepository,
    IAddLocationByCoordinateAndDateQuery addLocationByCoordinateAndDateQuery,
    ILogger<AddSatelliteMessengerLocationQuery> logger) : IAddSatelliteMessengerLocationQuery
{
    public async Task<string> Execute()
    {
        string message;

        // Check if TrackingEnabled setting is enabled.
        if (!(await settingsRepository.GetSettings()).TrackingEnabled)
        {
            message = "Not adding satellite location because TrackingEnabled is false";
            logger.LogInformation("{Message}", message);
            return message;
        }

        // Retrieve location from satellite messenger feed.
        var messengerlocation = await garminExploreMapShareManager.GetSatelliteMessengerLocation();

        // Check if messenger location is null.
        if (messengerlocation == null)
        {
            message = "Not adding satellite location because location is null";
            logger.LogInformation("{Message}", message);
            return message;
        }

        // Check if location has already been added
        var locations = await photographyRepository.GetHikerLocations();
        if (LocationsContainsSatelliteMessengerLocation(locations, messengerlocation))
        {
            message = "Not adding satellite location because location already is the most recent automatic location";
            logger.LogInformation("{Message}", message);
            return message;
        }

        // Add location
        await addLocationByCoordinateAndDateQuery.Execute(messengerlocation.Lat, messengerlocation.Lon, messengerlocation.Date);
        message = $"Added satellite location add Lat {messengerlocation.Lat}, Lon {messengerlocation.Lon}, Date {messengerlocation.Date}";
        logger.LogInformation("{Message}", message);
        return message;
    }

    private static bool LocationsContainsSatelliteMessengerLocation(
        IEnumerable<HikerLocation> locations, SatelliteMessengerLocation messengerLocation)
    {
        // Messenger location is never older than existing locations, so we can look at the most recent location
        var mostRecentLocation = locations
            .Where(location => !location.IsManual)
            .OrderByDescending(location => location.Date)
            .FirstOrDefault();

        if (mostRecentLocation == null) {
            return false;
        }

        if (mostRecentLocation.Date != messengerLocation.Date
            || mostRecentLocation.Lat != messengerLocation.Lat
            || mostRecentLocation.Lon != messengerLocation.Lon)
        {
            return false;
        }

        return true;
    }
}
