using Business.Components.Locations.Internal;
using Business.Entities.Dto;
using Business.Interfaces.Locations;
using Common.Common.Interfaces;
using Data.Interfaces;

namespace Business.Components.Locations;

public class AddAutomaticLocationQuery : IAddAutomaticLocationQuery
{
    private readonly IPhotographyRepository _photographyRepository;
    private readonly IPlacesRepository _placesRepository;
    private readonly ITrailRepository _trailRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IGetDistanceBetweenLocationsQuery _getDistanceBetweenLocationsQuery;
    private readonly double _minimumDistance = 1500.0; // Meters

    public AddAutomaticLocationQuery(
        IPhotographyRepository photographyRepository,
        IPlacesRepository placesRepository,
        ITrailRepository trailRepository,
        IDateTimeProvider dateTimeProvider,
        IGetDistanceBetweenLocationsQuery getDistanceBetweenLocationsQuery)
    {
        _photographyRepository = photographyRepository;
        _placesRepository = placesRepository;
        _trailRepository = trailRepository;
        _dateTimeProvider = dateTimeProvider;
        _getDistanceBetweenLocationsQuery = getDistanceBetweenLocationsQuery;
    }

    public async Task Execute(double lat, double lon)
    {
        var places = await _placesRepository.GetPlaces();

        var nearbyPlaces = places
            .Select(place => (Place: place, Distance: _getDistanceBetweenLocationsQuery.Execute(lat, lon, place.Lat, place.Lon)))
            .Where(placeDistance => placeDistance.Distance < _minimumDistance)
            .ToList();

        Place? closestPlace = nearbyPlaces.Count > 0
            ? nearbyPlaces.OrderBy(placeDistance => placeDistance.Distance).First().Place
            : null;

        if (closestPlace != null)
        {
            await _photographyRepository.AddHikerLocation(new HikerLocation(
                _dateTimeProvider.UtcNow,
                false,
                lat,
                lon,
                closestPlace?.Distance,
                closestPlace?.Id,
                closestPlace?.SectionId));

            return;
        }

        var marker = await GetClosestDistanceMarker(lat, lon);

        if (marker == null)
        {
            await _photographyRepository.AddHikerLocation(new HikerLocation(
                _dateTimeProvider.UtcNow,
                false,
                lat,
                lon,
                null,
                null,
                null));

            return;
        }

        var section = (await _photographyRepository.GetSections())
            .Where(section => section.StartDistance <= marker.Distance && section.EndDistance > marker.Distance)
            .FirstOrDefault();

        await _photographyRepository.AddHikerLocation(new HikerLocation(
            _dateTimeProvider.UtcNow,
            false,
            lat,
            lon,
            marker?.Distance,
            null,
            section?.Id));
    }

    private async Task<DistanceMarker?> GetClosestDistanceMarker(double lat, double lon)
    {
        var markers = await _trailRepository.GetTrail();
        var nearbyMarkers = markers
            .Select(marker => (Marker: marker, Distance: _getDistanceBetweenLocationsQuery.Execute(lat, lon, marker.Lat, marker.Lon)))
            .Where(markerDistance => markerDistance.Distance < _minimumDistance)
            .ToList();

        return nearbyMarkers.Count > 0
            ? nearbyMarkers.OrderBy(placeDistance => placeDistance.Distance).First().Marker
            : null;
    }
}
