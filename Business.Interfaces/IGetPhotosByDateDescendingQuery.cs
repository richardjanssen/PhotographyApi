using Business.Entities;

namespace Business.Interfaces;

public interface IGetPhotosByDateDescendingQuery
{
    IReadOnlyCollection<Photo> Execute();
}
