using Business.Entities.Dto;
using PhotographyApi.ViewModels.HikerUpdates;

namespace PhotographyApi.Mappers;

public static class HikerUpdateMapExtensions
{
    public static HikerUpdate Map(this AddHikerUpdateViewModel addHikerUpdate, DateTime date) => new(
        date,
        addHikerUpdate.Title,
        addHikerUpdate.Type,
        addHikerUpdate.Text,
        addHikerUpdate.Distance,
        addHikerUpdate.AlbumId,
        addHikerUpdate.PlaceId);

    public static HikerUpdate Map(this AddHikerUpdateViewModel addHikerUpdate, int id, DateTime date) => new(
        id,
        date,
        addHikerUpdate.Title,
        addHikerUpdate.Type,
        addHikerUpdate.Text,
        addHikerUpdate.Distance,
        addHikerUpdate.AlbumId,
        addHikerUpdate.PlaceId);

    public static HikerUpdateBasicViewModel MapToHikerUpdateBasic(this HikerUpdate hikerUpdate) => new(
        hikerUpdate.Id,
        hikerUpdate.Date,
        hikerUpdate.Title,
        hikerUpdate.Type,
        hikerUpdate.Distance);

    public static AddHikerUpdateViewModel Map(this HikerUpdate hikerUpdate) => new(
        hikerUpdate.Id,
        hikerUpdate.Title,
        hikerUpdate.Type,
        hikerUpdate.Text,
        hikerUpdate.Distance,
        hikerUpdate.AlbumId,
        hikerUpdate.PlaceId);
}
