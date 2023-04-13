using Business.Entities.Dto;
using PhotographyApi.ViewModels.HikerUpdates;

namespace PhotographyApi.Mappers;

public static class AddHikerUpdateMapExtensions
{
    public static HikerUpdate Map(this AddHikerUpdateViewModel addHikerUpdate) => new(
        addHikerUpdate.Title,
        addHikerUpdate.Type,
        addHikerUpdate.Text,
        addHikerUpdate.Distance,
        addHikerUpdate.AlbumId);
}
