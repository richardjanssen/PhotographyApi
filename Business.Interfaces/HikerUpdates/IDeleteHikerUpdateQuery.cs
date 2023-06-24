namespace Business.Interfaces.HikerUpdates;

public interface IDeleteHikerUpdateQuery
{
    Task Execute(int id);
}
