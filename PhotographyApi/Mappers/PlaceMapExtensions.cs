using Business.Entities.Dto;
using PhotographyApi.ViewModels.Places;

namespace PhotographyApi.Mappers;

public static class PlaceMapExtensions
{
    public static PlaceViewModel Map(this Place place) => new(place.Id, place.Distance, place.Title);
}
