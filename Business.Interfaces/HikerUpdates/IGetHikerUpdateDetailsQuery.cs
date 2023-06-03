using Business.Entities;

namespace Business.Interfaces.HikerUpdates;

public interface IGetHikerUpdateDetailsQuery
{
    Task<HikerUpdateDetails> Execute(int id);
}
