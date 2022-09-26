using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.Mappers;
using PhotographyApi.ViewModels;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class PhotosController : ControllerBase
{
    private readonly IGetPhotosByDateDescendingQuery _getPhotosByDateDescendingQuery;
    private readonly IAddPhotoQuery _addPhotoQuery;
    private readonly string _basePath = "api/Images";

    public PhotosController(
        IGetPhotosByDateDescendingQuery getPhotosByDateDescendingQuery,
        IAddPhotoQuery addPhotoQuery)
    {
        _getPhotosByDateDescendingQuery = getPhotosByDateDescendingQuery;
        _addPhotoQuery = addPhotoQuery;
    }

    [HttpGet]
    public async Task<IReadOnlyCollection<PhotoViewModel>> Get()
    {
        return (await _getPhotosByDateDescendingQuery.Execute()).Select(photo => photo.Map(_basePath)).ToList();
    }

    [HttpPost]
    public async Task<PhotoViewModel> UploadPhoto()
    {
        var formCollection = await Request.ReadFormAsync();
        var file = formCollection.Files[0];

        // Commented out until authorisation
        //return _addPhotoQuery.Execute(file).Map(_basePath);

        throw new NotImplementedException("Not implemented until authorisation");
    }
}