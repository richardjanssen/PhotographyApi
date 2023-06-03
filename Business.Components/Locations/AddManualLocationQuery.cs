using Business.Entities.Dto;
using Business.Interfaces.Locations;
using Data.Interfaces;

namespace Business.Components.Locations;

public class AddManualLocationQuery : IAddManualLocationQuery
{
    private readonly IPhotographyRepository _photographyRepository;

    public AddManualLocationQuery(IPhotographyRepository photographyRepository) => _photographyRepository = photographyRepository;

    public async Task Execute(double distance)
    {
        var location = new HikerLocation(DateTime.Now, distance, distance, true, null, null);
        await _photographyRepository.AddHikerLocation(location);
    }
}
