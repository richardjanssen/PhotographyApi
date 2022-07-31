using Business.Entities;

namespace Business.Interfaces;

public interface IGetPhotosByDateDescendingQuery
{
    Task<IReadOnlyCollection<Photo>> Execute();
}
