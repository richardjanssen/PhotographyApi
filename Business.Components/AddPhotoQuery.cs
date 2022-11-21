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
        new Size(800, 320),
        new Size(1600, 640),
        new Size(1920, 1200),
        new Size(2560, 1440),
        new Size(3840, 2160)
    };
}
