namespace Business.Interfaces.Locations;

public interface IAddManualLocationQuery
{
    Task Execute(double distance);
}
