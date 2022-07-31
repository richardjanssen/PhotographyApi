using Business.Entities;
using Business.Interfaces;
using Data.Repository.Interfaces;

namespace Business.Components;

public class GetPhotosByDateDescendingQuery : IGetPhotosByDateDescendingQuery
{
    private readonly IPhotographyRepository _photographyRepository;

    public GetPhotosByDateDescendingQuery(IPhotographyRepository photographyRepository) => 
        _photographyRepository = photographyRepository;

    public async Task<IReadOnlyCollection<Photo>> Execute()
    {
        return (await _photographyRepository.GetPhotos()).OrderByDescending(photo => photo.Date).ToList();
    }
}
