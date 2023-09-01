namespace Business.Interfaces.Locations;

public interface IAddAutomaticLocationQuery
{
    Task Execute(double lat, double lon);
}
