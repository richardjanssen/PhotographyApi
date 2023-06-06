namespace Business.Interfaces.Locations;

public interface IDeleteLocationQuery
{
    Task Execute(int id);
}
