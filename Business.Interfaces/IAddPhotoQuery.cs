using Business.Entities;
using Microsoft.AspNetCore.Http;

namespace Business.Interfaces;

public interface IAddPhotoQuery
{
    Photo Execute(IFormFile file);
}
