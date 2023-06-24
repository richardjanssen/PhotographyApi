using Business.Entities.Dto;
using Business.Interfaces.Locations;
using Data.Interfaces;

namespace Business.Components.Locations;

public class GetLocationsQuery : IGetLocationsQuery
{ 
    private readonly IPhotographyRepository _photographyRepository;

    public GetLocationsQuery(IPhotographyRepository photographyRepository) => _photographyRepository = photographyRepository;

    public async Task<IReadOnlyCollection<HikerLocation>> Execute() => (await _photographyRepository.GetHikerLocations())
        .OrderByDescending(location => location.Date)
        .ToList();
}
