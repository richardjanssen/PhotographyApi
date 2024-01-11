using Business.Entities;
using Business.Entities.Dto;

namespace Business.Interfaces;

public interface IAddPhotoQuery
{
    Task<Photo> Execute(AddPhoto addPhoto);
}
