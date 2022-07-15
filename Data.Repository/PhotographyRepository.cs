using Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class PhotographyRepository : IPhotographyRepository
{
    public PhotographyRepository(PhotographyContext photographyContext) => _photographyContext = photographyContext;

    private readonly PhotographyContext _photographyContext;

    public IEnumerable<Business.Entities.Photo> GetPhotos() => _photographyContext.Photos
        .Include(photo => photo.Images)
        .Select(photo => photo.Map())
        .ToList();

    public Business.Entities.Photo AddPhoto(Business.Entities.Photo photo)
    {
        var result = _photographyContext.Add(photo.Map()).Entity;
        _photographyContext.SaveChanges();

        return result.Map();
    }
}
