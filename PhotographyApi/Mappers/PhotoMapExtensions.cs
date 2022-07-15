using Business.Entities;
using PhotographyApi.ViewModels;

namespace PhotographyApi.Mappers;

public static class PhotoMapExtensions
{
    public static PhotoViewModel Map(this Photo photo) =>
        new(photo.Id, photo.Date, photo.Images.Select(image => image.Map()).ToList());

    public static ImageViewModel Map(this Image image) =>
        new(image.WidthPx, image.HeightPx, image.Path);

    public static AddPhoto Map(this AddPhotoViewModel photo) =>
        new(photo.Images.Select(image => image.Map()).ToList());

    public static Image Map(this ImageViewModel image) =>
        new(image.WidthPx, image.HeightPx, image.Path);
}
