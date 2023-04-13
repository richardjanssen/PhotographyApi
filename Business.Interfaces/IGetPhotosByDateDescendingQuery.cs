using Business.Entities.Dto;

namespace Business.Interfaces;

public interface IGetPhotosByDateDescendingQuery
{
    Task<IReadOnlyCollection<Photo>> Execute();
}
