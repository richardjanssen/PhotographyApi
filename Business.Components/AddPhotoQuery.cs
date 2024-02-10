using Business.Components.Internal;
using Business.Entities;
using Business.Entities.Dto;
using Business.Interfaces;
using Common.Common.Interfaces;
using Data.Interfaces;

namespace Business.Components;

public class AddPhotoQuery(
    IPhotographyRepository photographyRepository,
    IDateTimeProvider dateTimeProvider,
    ISaveImageToFolderQuery saveImageToFolderQuery) : IAddPhotoQuery
{
    private readonly List<Size> _resizeLimits = GetResizeLimits();

    public async Task<Photo> Execute(AddPhoto addPhoto)
    {
        if ((await photographyRepository.GetAlbums()).All(album => album.Id != addPhoto.AlbumId))
        {
            throw new InvalidOperationException($"Cannot add photo to album with id {addPhoto.AlbumId}. Album does not exist");
        };

        var images = saveImageToFolderQuery.Execute(addPhoto.Image, _resizeLimits);
        var date = dateTimeProvider.UtcNow;
        var photo = new Photo(date, images);

        return await photographyRepository.AddAlbumPhoto(photo, addPhoto.AlbumId);
    }

    private static List<Size> GetResizeLimits() => new() {
        new Size(840, 336),
        new Size(1680, 672),
        new Size(2016, 1260),
        new Size(2688, 1512),
        new Size(4032, 2268)
    };
}
