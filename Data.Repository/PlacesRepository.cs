using Business.Entities.Dto;
using Data.Repository;
using Data.Repository.Interfaces;

namespace Data.Interfaces;

public class PlacesRepository(IPhotographyManager photographyManager) : IPlacesRepository
{
    public async Task<IEnumerable<Place>> GetPlaces() => (await photographyManager.GetPlaces()).Select(place => place.Map()).ToList();
}
