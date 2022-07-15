using Data.Repository.Entities;

namespace Data.Repository;

public static class PhotographyRepositoryMapExtensions
{

    public static Business.Entities.Photo Map(this Photo photo) =>
        new(photo.Id, photo.Date, photo.Images.Select(image => image.Map()).ToList());

    public static Business.Entities.Image Map(this Image image) =>
        new(image.WidthPx, image.HeightPx, image.Path);

    public static Photo Map(this Business.Entities.Photo photo) =>
        new()
        {
            Date = photo.Date,
            Images = photo.Images.Select(image => image.Map()).ToList()
        };

    public static Image Map(this Business.Entities.Image image) =>
        new()
        {
            WidthPx = image.WidthPx,
            HeightPx = image.HeightPx,
            Path = image.Path
        };
}
