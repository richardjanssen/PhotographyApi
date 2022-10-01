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

    public AddPhotoQuery(
        IPhotographyRepository photographyRepository,
        IDateTimeProvider dateTimeProvider,
        ISaveImageToFolderQuery saveImageToFolderQuery)
    {
        _photographyRepository = photographyRepository;
        _dateTimeProvider = dateTimeProvider;
        _saveImageToFolderQuery = saveImageToFolderQuery;
    }

    public Photo Execute(IFormFile file)
    {
        var image = _saveImageToFolderQuery.Execute(file);
        var date = _dateTimeProvider.UtcNow;
        var photo = new Photo(null, date, new List<Image> { image });
        return _photographyRepository.AddPhoto(photo);
    }
}
