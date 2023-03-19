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

    public static Business.Entities.Album Map(this Album album) => new(album.Id, album.Title);

    public static Album Map(this Business.Entities.Album album, int id, string fileName) =>
    new()
    {
        Id = id,
        Title = album.Title,
        FileName = fileName
    };

    public static Business.Entities.AddHikerUpdate Map(this HikerUpdate hikerUpdate) => new(
        hikerUpdate.Id,
        hikerUpdate.Title,
        hikerUpdate.Type,
        hikerUpdate.Text,
        hikerUpdate.Distance,
        hikerUpdate.AlbumId);

    public static HikerUpdate Map(this Business.Entities.AddHikerUpdate addHikerUpdate, int id) =>
    new()
    {
        Id = id,
        Title = addHikerUpdate.Title,
        Type = addHikerUpdate.Type,
        Text = addHikerUpdate.Text,
        Distance = addHikerUpdate.Distance,
        AlbumId = addHikerUpdate.AlbumId
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
