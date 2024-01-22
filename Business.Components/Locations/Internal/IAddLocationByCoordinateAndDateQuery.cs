namespace Business.Components.Locations.Internal;

public interface IAddLocationByCoordinateAndDateQuery
{
    Task Execute(double lat, double lon, DateTime date);
}
