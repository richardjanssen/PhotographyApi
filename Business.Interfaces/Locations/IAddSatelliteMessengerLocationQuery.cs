namespace Business.Interfaces.Locations;

public interface IAddSatelliteMessengerLocationQuery
{
    Task<string> Execute();
}