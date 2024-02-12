using Business.Entities.Dto;
using Business.Entities.Locations;
using PhotographyApi.ViewModels.Locations;

namespace PhotographyApi.Mappers;

public static class LocationMapExtensions
{
    public static LocationViewModel Map(this HikerLocation location) => new(
        location.Id,
        location.Date,
        location.IsManual,
        location.Lat,
        location.Lon,
        location.Distance,
        location.PlaceId,
        location.SectionId);

    public static MapLocationsViewModel Map(this MapLocations mapLocations) => new(mapLocations.CurrentLocation?.Map(), mapLocations.HistoricLocations.Select(location => location.Map()).ToList());
    private static CoordinateViewModel Map(this Coordinate coordinate) => new(coordinate.Lat, coordinate.Lon);
}
