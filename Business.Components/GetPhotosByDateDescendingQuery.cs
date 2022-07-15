using Business.Entities;
using Business.Interfaces;
using Data.Repository.Interfaces;

namespace Business.Components;

public class GetPhotosByDateDescendingQuery : IGetPhotosByDateDescendingQuery
{
    private readonly IPhotographyRepository _photographyRepository;

    public GetPhotosByDateDescendingQuery(IPhotographyRepository photographyRepository) => 
        _photographyRepository = photographyRepository;

    public IReadOnlyCollection<Photo> Execute() => 
        _photographyRepository.GetPhotos().OrderByDescending(photo => photo.Date).ToList();
}
