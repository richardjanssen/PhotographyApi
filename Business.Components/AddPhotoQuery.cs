using Business.Entities;
using Business.Interfaces;
using Common.Common.Interfaces;
using Data.Repository.Interfaces;

namespace Business.Components;

public class AddPhotoQuery : IAddPhotoQuery
{
    private readonly IPhotographyRepository _photographyRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AddPhotoQuery(IPhotographyRepository photographyRepository, IDateTimeProvider dateTimeProvider)
    {
        _photographyRepository = photographyRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public Photo Execute(AddPhoto photo)
    {
        return _photographyRepository.AddPhoto(new Photo(null, _dateTimeProvider.UtcNow, photo.Images));
    }
}
