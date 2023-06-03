using Business.Entities.Dto;

namespace Business.Interfaces.Locations;

public interface IGetLocationsQuery
{
    Task<IReadOnlyCollection<HikerLocation>> Execute();
}
