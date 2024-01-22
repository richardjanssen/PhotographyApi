using Business.Components.Locations.Internal;
using Business.Entities;
using Business.Entities.Dto;
using Business.Interfaces.Locations;
using Common.Common.Interfaces;
using Data.Interfaces;
using Data.Proxies.GarminExploreMapShare;

namespace Business.Components.Locations;
public class AddSatelliteMessengerLocationQuery(
    IPhotographyRepository photographyRepository,
    IDateTimeProvider dateTimeProvider,
    IGarminExploreMapShareManager garminExploreMapShareManager,
    ISettingsRepository settingsRepository,
    IAddLocationByCoordinateAndDateQuery addLocationByCoordinateAndDateQuery) : IAddSatelliteMessengerLocationQuery
{
    public async Task Execute()
    {
        // Check if TrackingEnabled setting is enabled.
        if (!(await settingsRepository.GetSettings()).TrackingEnabled)
        {
            return;
        }

        var locations = await photographyRepository.GetHikerLocations();

        // Check if last location > 30 minutes ago. If not, do nothing. If so, continue.
        if (HasRecentAutomaticLocation(locations))
        {
            return;
        }

        // Retrieve location from satellite messenger feed.
        var messengerlocation = await garminExploreMapShareManager.GetSatelliteMessengerLocation();

        // Check if messenger location has already been added.
        if (messengerlocation != null && !ContainsSatelliteMessengerLocation(locations, messengerlocation))
        {
            // Add location
            await addLocationByCoordinateAndDateQuery.Execute(messengerlocation.Lat, messengerlocation.Lon, messengerlocation.Date);
        }
    }

    private bool HasRecentAutomaticLocation(IEnumerable<HikerLocation> locations)
    {
        var now = dateTimeProvider.UtcNow;
        return locations.Any(location => !location.IsManual && now.Subtract(location.Date).TotalMinutes < 30);
    }

    private bool ContainsSatelliteMessengerLocation(IEnumerable<HikerLocation> locations, SatelliteMessengerLocation messengerLocation)
    {
        // We know that a new location is always newer than existing locations, so we can look at the most recent location
        var mostRecentLocation = locations.OrderByDescending(location => location.Date).FirstOrDefault();

        if (mostRecentLocation == null) {
            return false;
        }

        if (mostRecentLocation.Date != messengerLocation.Date || mostRecentLocation.Lat != messengerLocation.Lat || mostRecentLocation.Lon != messengerLocation.Lon)
        {
            return false;
        }

        return true;
    }
}
