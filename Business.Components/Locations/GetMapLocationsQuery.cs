using Business.Components.Locations.Internal;
using Business.Entities.Locations;
using Business.Interfaces.Locations;
using Data.Interfaces;

namespace Business.Components.Locations;

public class GetMapLocationsQuery(IPhotographyRepository photographyRepository) : IGetMapLocationsQuery
{
    public async Task<MapLocations> Execute(int locationId)
    {
        var locations = await photographyRepository.GetHikerLocations();
        var currentLocation = locations.FirstOrDefault(location => location.Id == locationId);

        if (currentLocation == null)
        {
            return new MapLocations(null, []);
        }
        var earliestHistoricLocationDate = currentLocation.Date.AddHours(-168); // 1 week
        var historicLocations = locations
            .Where(location => !location.IsManual && location.Date > earliestHistoricLocationDate && location.Date <= currentLocation.Date)
            .OrderByDescending(location => location.Date)
            .ToList();

        return new MapLocations(currentLocation.Map(), historicLocations.Select(location => location.Map()).ToList());
    }
}
