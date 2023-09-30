using Business.Entities.Dto;

namespace Data.Interfaces;

public interface IPlacesRepository
{
    Task<IEnumerable<Place>> GetPlaces();
}
