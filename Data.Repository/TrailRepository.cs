using Business.Entities.Dto;
using Data.Repository;
using Data.Repository.Interfaces;

namespace Data.Interfaces;

public class TrailRepository : ITrailRepository
{
    private readonly IPhotographyManager _photographyManager;

    public TrailRepository(IPhotographyManager photographyManager) => _photographyManager = photographyManager;

    public async Task<IReadOnlyCollection<DistanceMarker>> GetTrail() =>
        (await _photographyManager.GetTrail()).Select(marker => marker.Map()).ToList();
}
