using Business.Entities;
using Business.Entities.Dto;

namespace Business.Interfaces.HikerUpdates;

public interface IGetHikerUpdatesQuery
{
    Task<IReadOnlyCollection<HikerUpdate>> Execute();
}
