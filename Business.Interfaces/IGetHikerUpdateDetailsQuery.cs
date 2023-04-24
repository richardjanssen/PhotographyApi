using Business.Entities;

namespace Business.Interfaces;

public interface IGetHikerUpdateDetailsQuery
{
    Task<HikerUpdateDetails> Execute(int id);
}
