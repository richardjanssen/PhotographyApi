using Business.Entities.Dto;
using PhotographyApi.ViewModels;
using PhotographyApi.ViewModels.Albums;

namespace PhotographyApi.Mappers;

public static class AlbumMapExtensions
{
    public static Album Map(this AddAlbumViewModel album) => new(album.Title);
    public static AlbumViewModel Map(this Album album) => new(album.Id, album.Title);

    public static AlbumDetailsViewModel Map(this AlbumDetails albumDetails, string basePath) =>
        new(albumDetails.Photos.Select(photo => photo.Map(basePath)).ToList());
}
