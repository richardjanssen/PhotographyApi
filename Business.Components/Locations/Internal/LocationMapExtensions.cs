using Business.Entities.Dto;
using Business.Entities.Locations;

namespace Business.Components.Locations.Internal;
public static class LocationMapExtensions
{
    public static Coordinate Map(this HikerLocation location) => new(location.Lat, location.Lon);
}
