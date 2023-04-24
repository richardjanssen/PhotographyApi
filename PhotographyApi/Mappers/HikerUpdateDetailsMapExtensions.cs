using Business.Entities;
using Common.Common;
using PhotographyApi.ViewModels.HikerUpdates;

namespace PhotographyApi.Mappers;

public static class HikerUpdateDetailsMapExtensions
{
    public static HikerUpdateDetailsViewModel Map(this HikerUpdateDetails update) =>
        new(update.Text, update.Album?.Map(Constants.PhotosBasePath));
}
