using Business.Entities.Dto;
using Data.Repository;
using Data.Repository.Interfaces;

namespace Data.Interfaces;

public class PlacesRepository : IPlacesRepository
{
    private readonly IPhotographyManager _photographyManager;

    public PlacesRepository(IPhotographyManager photographyManager) => _photographyManager = photographyManager;

    public async Task<IEnumerable<Place>> GetPlaces() => (await _photographyManager.GetPlaces()).Select(place => place.Map()).ToList();
}
