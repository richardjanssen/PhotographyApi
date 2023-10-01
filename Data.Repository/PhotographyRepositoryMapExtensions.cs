using Data.Repository.Entities;

namespace Data.Repository;

public static class PhotographyRepositoryMapExtensions
{
    public static Business.Entities.Dto.Photo Map(this Photo photo) =>
        new(photo.Id, photo.Date, photo.Images.Select(image => image.Map()).ToList());

    public static Photo Map(this Business.Entities.Dto.Photo photo, int id) =>
    new()
    {
        Id = id,
        Date = photo.Date,
        Images = photo.Images.Select(image => image.Map()).ToList()
    };

    public static Business.Entities.Dto.Album Map(this Album album) => new(album.Id, album.Title);

    public static Album Map(this Business.Entities.Dto.Album album, int id, string fileName) =>
    new()
    {
        Id = id,
        Title = album.Title,
        FileName = fileName
    };

    public static Business.Entities.Dto.AlbumDetails Map(this AlbumDetails albumDetails) =>
        new(albumDetails.Photos.Select(photo => photo.Map()).ToList());

    public static Business.Entities.Dto.Section Map(this Section section) =>
        new(section.Id, section.Title, section.StartDistance, section.EndDistance);

    public static Business.Entities.Dto.Place Map(this Place place) =>
        new(place.Id, place.SectionId, place.Type, place.Title, place.Distance, place.Lat, place.Lon);

    public static Business.Entities.Dto.HikerUpdate Map(this HikerUpdate hikerUpdate) => new(
        hikerUpdate.Id,
        hikerUpdate.Date,
        hikerUpdate.Title,
        hikerUpdate.Type,
        hikerUpdate.Text,
        hikerUpdate.Distance,
        hikerUpdate.AlbumId,
        hikerUpdate.PlaceId);

    public static HikerUpdate Map(this Business.Entities.Dto.HikerUpdate addHikerUpdate, int id) =>
        new()
        {
            Id = id,
            Date = addHikerUpdate.Date,
            Title = addHikerUpdate.Title,
            Type = addHikerUpdate.Type,
            Text = addHikerUpdate.Text,
            Distance = addHikerUpdate.Distance,
            AlbumId = addHikerUpdate.AlbumId,
            PlaceId = addHikerUpdate.PlaceId
        };

    public static HikerLocation Map(this Business.Entities.Dto.HikerLocation hikerLocation, int id) =>
    new()
    {
        Id = id,
        Date = hikerLocation.Date,
        IsManual = hikerLocation.IsManual,
        Lat = hikerLocation.Lat,
        Lon = hikerLocation.Lon,
        PlaceId = hikerLocation.PlaceId
    };

    public static Business.Entities.Dto.HikerLocation Map(this HikerLocation hikerLocation) =>
        new(hikerLocation.Id,
            hikerLocation.Date,
            hikerLocation.IsManual,
            hikerLocation.Lat,
            hikerLocation.Lon,
            hikerLocation.PlaceId);

    public static Business.Entities.Dto.Settings Map(this Settings settings) => new(settings.TrackingEnabled, settings.MapboxEnabled);

    public static Settings Map(this Business.Entities.Dto.Settings settings) => new()
    {
        TrackingEnabled = settings.TrackingEnabled,
        MapboxEnabled = settings.MapboxEnabled
    };

    public static Business.Entities.Dto.Account Map(this Account account) =>
        new(account.UserName, account.PasswordHash, account.Salt);

    private static Business.Entities.Dto.Image Map(this Image image) =>
        new(image.WidthPx, image.HeightPx, image.Guid, image.Extension);

    private static Image Map(this Business.Entities.Dto.Image image) =>
        new()
        {
            WidthPx = image.WidthPx,
            HeightPx = image.HeightPx,
            Guid = image.Guid,
            Extension = image.Extension
        };
}
