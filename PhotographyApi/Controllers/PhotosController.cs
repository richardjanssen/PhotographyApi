using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.Mappers;
using PhotographyApi.ViewModels;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class PhotosController : ControllerBase
{
    private readonly IGetPhotosByDateDescendingQuery _getPhotosByDateDescendingQuery;
    private readonly IAddPhotoQuery _addPhotoQuery;

    public PhotosController(IGetPhotosByDateDescendingQuery getPhotosByDateDescendingQuery, IAddPhotoQuery addPhotoQuery)
    {
        _getPhotosByDateDescendingQuery = getPhotosByDateDescendingQuery;
        _addPhotoQuery = addPhotoQuery;
    }

    [HttpGet]
    public IReadOnlyCollection<PhotoViewModel> Get() => _getPhotosByDateDescendingQuery.Execute().Select(photo => photo.Map()).ToList();

    [HttpPost]
    public PhotoViewModel AddPhoto(AddPhotoViewModel photo) => _addPhotoQuery.Execute(photo.Map()).Map();
}