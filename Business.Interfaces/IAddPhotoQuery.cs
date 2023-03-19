using Business.Entities;
using Microsoft.AspNetCore.Http;

namespace Business.Interfaces;

public interface IAddPhotoQuery
{
    Task<Photo> Execute(AddPhoto addPhoto);
}
