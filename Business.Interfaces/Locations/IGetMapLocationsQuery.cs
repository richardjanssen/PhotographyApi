using Business.Entities.Locations;

namespace Business.Interfaces.Locations;
public interface IGetMapLocationsQuery
{
    Task<MapLocations> Execute(int locationId);
}