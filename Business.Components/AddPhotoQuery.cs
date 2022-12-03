using Business.Components.Internal;
using Business.Entities;
using Business.Interfaces;
using Common.Common.Interfaces;
using Data.Repository.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Business.Components;

public class AddPhotoQuery : IAddPhotoQuery
{
    private readonly IPhotographyRepository _photographyRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ISaveImageToFolderQuery _saveImageToFolderQuery;
    private readonly List<Size> _resizeLimits;

    public AddPhotoQuery(
        IPhotographyRepository photographyRepository,
        IDateTimeProvider dateTimeProvider,
        ISaveImageToFolderQuery saveImageToFolderQuery)
    {
        _photographyRepository = photographyRepository;
        _dateTimeProvider = dateTimeProvider;
        _saveImageToFolderQuery = saveImageToFolderQuery;
        _resizeLimits = GetResizeLimits();
    }

    public async Task<Photo> Execute(IFormFile file)
    {
        var images = _saveImageToFolderQuery.Execute(file, _resizeLimits);
        var date = _dateTimeProvider.UtcNow;
        var photo = new Photo(null, date, images);
        return await _photographyRepository.AddPhoto(photo);
    }

    private static List<Size> GetResizeLimits() => new() {
        new Size(840, 336),
        new Size(1680, 672),
        new Size(2016, 1260),
        new Size(2688, 1512),
        new Size(4032, 2268)
    };
}
