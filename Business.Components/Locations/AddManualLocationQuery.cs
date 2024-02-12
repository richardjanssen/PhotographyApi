using Business.Entities.Dto;
using Business.Interfaces.Locations;
using Common.Common.Interfaces;
using Data.Interfaces;

namespace Business.Components.Locations;

public class AddManualLocationQuery(
    IPhotographyRepository photographyRepository,
    IDateTimeProvider dateTimeProvider,
    IPlacesRepository placesRepository) : IAddManualLocationQuery
{
    public async Task Execute(int placeId)
    {
        var places = await placesRepository.GetPlaces();
        var place = places.First(place => place.Id == placeId);
        var location = new HikerLocation(dateTimeProvider.UtcNow, true, place.Lat, place.Lon, place.Distance, placeId, place.SectionId);
        await photographyRepository.AddHikerLocation(location);
    }
}
