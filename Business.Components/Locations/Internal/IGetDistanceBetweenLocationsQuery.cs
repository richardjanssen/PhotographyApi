namespace Business.Components.Locations.Internal;

public interface IGetDistanceBetweenLocationsQuery
{
    public double Execute(double lat1, double lon1, double lat2, double lon2);
}
