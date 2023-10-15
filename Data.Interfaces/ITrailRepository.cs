using Business.Entities.Dto;

namespace Data.Interfaces;
public interface ITrailRepository
{
    Task<IReadOnlyCollection<DistanceMarker>> GetTrail();
}