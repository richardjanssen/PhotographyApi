using Business.Entities.Dto;
using Business.Interfaces.Locations;
using Common.Common.Interfaces;
using Data.Interfaces;

namespace Business.Components.Locations;

public class AddManualLocationQuery : IAddManualLocationQuery
{
    private readonly IPhotographyRepository _photographyRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IPlacesRepository _placesRepository;

    public AddManualLocationQuery(IPhotographyRepository photographyRepository, IDateTimeProvider dateTimeProvider, IPlacesRepository placesRepository)
    {
        _photographyRepository = photographyRepository;
        _dateTimeProvider = dateTimeProvider;
        _placesRepository = placesRepository;
    }

    public async Task Execute(int placeId)
    {
        var places = await _placesRepository.GetPlaces();
        var place = places.First(place => place.Id == placeId);
        var location = new HikerLocation(dateTimeProvider.UtcNow, true, place.Lat, place.Lon, place.Distance, placeId, place.SectionId);
        await photographyRepository.AddHikerLocation(location);
    }
}
