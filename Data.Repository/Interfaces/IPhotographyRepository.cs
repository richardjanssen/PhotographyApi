using Business.Entities;

namespace Data.Repository.Interfaces;

public interface IPhotographyRepository
{
    IEnumerable<Photo> GetPhotos();
    Photo AddPhoto(Photo photo);
}
