using Business.Entities;
using PhotographyApi.ViewModels;

namespace PhotographyApi.Mappers;

public static class PhotoMapExtensions
{
    public static PhotoViewModel Map(this Photo photo, string basePath) =>
        new(photo.Id, photo.Date, photo.Images.Select(image => image.Map(basePath)).ToList());

    public static ImageWithPathViewModel Map(this Image image, string basePath) =>
        new(image.WidthPx, image.HeightPx, $"{basePath}/{image.Guid}{image.Extension}");
}
