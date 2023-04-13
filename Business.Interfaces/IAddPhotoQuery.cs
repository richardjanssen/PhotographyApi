using Business.Entities;
using Business.Entities.Dto;
using Microsoft.AspNetCore.Http;

namespace Business.Interfaces;

public interface IAddPhotoQuery
{
    Task<Photo> Execute(AddPhoto addPhoto);
}
