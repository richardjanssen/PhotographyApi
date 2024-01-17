using Business.Interfaces.Locations;
using Common.Common.Interfaces;
using Data.Interfaces;
using Data.Proxies.GarminExploreMapShare;

namespace Business.Components.Locations;
public class AddSatelliteMessengerLocationQuery : IAddSatelliteMessengerLocationQuery
{
    private readonly IPhotographyRepository _photographyRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IGarminExploreMapShareManager _garminExploreMapShareManager;

    public AddSatelliteMessengerLocationQuery(
        IPhotographyRepository photographyRepository,
        IDateTimeProvider dateTimeProvider,
        IGarminExploreMapShareManager garminExploreMapShareManager)
    {
        _photographyRepository = photographyRepository;
        _dateTimeProvider = dateTimeProvider;
        _garminExploreMapShareManager = garminExploreMapShareManager;
    }

    public async Task Execute()
    {
        // Check if last location > 30 minutes ago. If not, do nothing. If so, continue.
        if (await HasRecentAutomaticLocation())
        {
            return;
        }

        // Retrieve location from satellite messenger feed.
        var satelliteMessengerlocation = await _garminExploreMapShareManager.GetSatelliteMessengerLocation();
        throw new NotImplementedException();

        // Check if location and date have already been added. If so, do nothing. If not, continue.

        // Add location to locations based on lat lon and date.
    }

    private async Task<bool> HasRecentAutomaticLocation()
    {
        // Temporarily add two years because our mocked locations are in the future...
        var now = _dateTimeProvider.UtcNow.AddYears(2);
        return (await _photographyRepository.GetHikerLocations())
            .Any(location => !location.IsManual && now.Subtract(location.Date).TotalMinutes < 30);
    }
}
