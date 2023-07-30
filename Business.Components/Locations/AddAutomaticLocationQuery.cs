using Business.Entities.Dto;
using Business.Interfaces.Locations;
using Common.Common.Interfaces;
using Data.Interfaces;

namespace Business.Components.Locations;

public class AddAutomaticLocationQuery : IAddAutomaticLocationQuery
{
    private readonly IPhotographyRepository _photographyRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AddAutomaticLocationQuery(IPhotographyRepository photographyRepository, IDateTimeProvider dateTimeProvider)
    {
        _photographyRepository = photographyRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    // This method will be changed to work with lat, lon
    public async Task Execute(int placeId)
    {
        var location = new HikerLocation(_dateTimeProvider.UtcNow, false, null, null, placeId);
        await _photographyRepository.AddHikerLocation(location);
    }
}
