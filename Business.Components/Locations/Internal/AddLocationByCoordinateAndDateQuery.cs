using Business.Entities.Dto;
using Data.Interfaces;

namespace Business.Components.Locations.Internal;

public class AddLocationByCoordinateAndDateQuery(
    IPhotographyRepository photographyRepository,
    IPlacesRepository placesRepository,
    ITrailRepository trailRepository,
    IGetDistanceBetweenLocationsQuery getDistanceBetweenLocationsQuery) : IAddLocationByCoordinateAndDateQuery
{
    private readonly double _minimumDistance = 1500.0; // Meters

    public async Task Execute(double lat, double lon, DateTime date)
    {
        var places = await placesRepository.GetPlaces();

        var nearbyPlaces = places
            .Select(place => (Place: place, Distance: getDistanceBetweenLocationsQuery.Execute(lat, lon, place.Lat, place.Lon)))
            .Where(placeDistance => placeDistance.Distance < _minimumDistance)
            .ToList();

        Place? closestPlace = nearbyPlaces.Count > 0
            ? nearbyPlaces.OrderBy(placeDistance => placeDistance.Distance).First().Place
            : null;

        if (closestPlace != null)
        {
            await photographyRepository.AddHikerLocation(new HikerLocation(
                date,
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
            await photographyRepository.AddHikerLocation(new HikerLocation(
                date,
                false,
                lat,
                lon,
                null,
                null,
                null));

            return;
        }

        var section = (await photographyRepository.GetSections())
            .Where(section => section.StartDistance <= marker.Distance && section.EndDistance > marker.Distance)
            .FirstOrDefault();

        await photographyRepository.AddHikerLocation(new HikerLocation(
            date,
            false,
            lat,
            lon,
            marker?.Distance,
            null,
            section?.Id));
    }

    private async Task<DistanceMarker?> GetClosestDistanceMarker(double lat, double lon)
    {
        var markers = await trailRepository.GetTrail();
        var nearbyMarkers = markers
            .Select(marker => (Marker: marker, Distance: getDistanceBetweenLocationsQuery.Execute(lat, lon, marker.Lat, marker.Lon)))
            .Where(markerDistance => markerDistance.Distance < _minimumDistance)
            .ToList();

        return nearbyMarkers.Count > 0
            ? nearbyMarkers.OrderBy(placeDistance => placeDistance.Distance).First().Marker
            : null;
    }
}
