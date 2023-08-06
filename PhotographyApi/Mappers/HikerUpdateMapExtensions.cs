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
        addHikerUpdate.AlbumId);

    public static HikerUpdateBasicViewModel Map(this HikerUpdate hikerUpdate) => new(
        hikerUpdate.Id,
        hikerUpdate.Date,
        hikerUpdate.Title,
        hikerUpdate.Type,
        hikerUpdate.Distance);
}
