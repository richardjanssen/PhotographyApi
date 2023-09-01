using Business.Components.Locations.Internal;
using Business.Entities.Dto;
using Business.Interfaces.Locations;
using Common.Common.Interfaces;
using Data.Interfaces;

namespace Business.Components.Locations;

public class AddAutomaticLocationQuery : IAddAutomaticLocationQuery
{
    private readonly IPhotographyRepository _photographyRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IGetDistanceBetweenLocationsQuery _getDistanceBetweenLocationsQuery;
    private readonly double _minimumDistance = 1500.0; // Meters

    public AddAutomaticLocationQuery(
        IPhotographyRepository photographyRepository,
        IDateTimeProvider dateTimeProvider,
        IGetDistanceBetweenLocationsQuery getDistanceBetweenLocationsQuery)
    {
        _photographyRepository = photographyRepository;
        _dateTimeProvider = dateTimeProvider;
        _getDistanceBetweenLocationsQuery = getDistanceBetweenLocationsQuery;
    }

    public async Task Execute(double lat, double lon)
    {
        var places = await _photographyRepository.GetPlaces();

        var nearbyPlaces = places
            .Select(place => (PlaceId: place.Id, Distance: _getDistanceBetweenLocationsQuery.Execute(lat, lon, place.Lat, place.Lon)))
            .Where(placeDistance => placeDistance.Distance < _minimumDistance)
            .ToList();

        int? closestPlaceId = nearbyPlaces.Count > 0
            ? nearbyPlaces.OrderBy(placeDistance => placeDistance.Distance).First().PlaceId
            : null;

        await _photographyRepository.AddHikerLocation(new HikerLocation(_dateTimeProvider.UtcNow, false, lat, lon, closestPlaceId));
    }
}
