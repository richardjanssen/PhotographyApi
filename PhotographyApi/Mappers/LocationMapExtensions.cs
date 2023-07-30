using Business.Entities.Dto;
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
        location.PlaceId);
}
