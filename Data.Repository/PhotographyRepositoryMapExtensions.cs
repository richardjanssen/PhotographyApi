using Data.Repository.Entities;

namespace Data.Repository;

public static class PhotographyRepositoryMapExtensions
{
    public static Business.Entities.Photo Map(this Photo photo) =>
        new(photo.Id, photo.Date, photo.Images.Select(image => image.Map()).ToList());

    public static Photo Map(this Business.Entities.Photo photo, int id) =>
    new()
    {
        Id = id,
        Date = photo.Date,
        Images = photo.Images.Select(image => image.Map()).ToList()
    };

    public static Business.Entities.Account Map(this Account account) =>
        new(account.UserName, account.PasswordHash, account.Salt);

    private static Business.Entities.Image Map(this Image image) =>
        new(image.WidthPx, image.HeightPx, image.Guid, image.Extension);

    private static Image Map(this Business.Entities.Image image) =>
        new()
        {
            WidthPx = image.WidthPx,
            HeightPx = image.HeightPx,
            Guid = image.Guid,
            Extension = image.Extension
        };
}
