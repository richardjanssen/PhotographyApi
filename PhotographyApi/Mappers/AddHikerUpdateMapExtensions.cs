using Business.Entities;
using PhotographyApi.ViewModels;

namespace PhotographyApi.Mappers;

public static class AddHikerUpdateMapExtensions
{
    public static AddHikerUpdate Map(this AddHikerUpdateViewModel addHikerUpdate) => new(
        addHikerUpdate.Id,
        addHikerUpdate.Title,
        addHikerUpdate.Type,
        addHikerUpdate.Text,
        addHikerUpdate.Distance,
        addHikerUpdate.AlbumId);
}
