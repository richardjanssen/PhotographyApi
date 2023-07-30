namespace Business.Interfaces.Locations;

public interface IAddAutomaticLocationQuery
{
    Task Execute(int placeId);
}
