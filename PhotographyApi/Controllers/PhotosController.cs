using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.Mappers;
using PhotographyApi.ViewModels;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class PhotosController : ControllerBase
{
    private readonly ILogger<PhotosController> _logger;
    private readonly IGetPhotosByDateDescendingQuery _getPhotosByDateDescendingQuery;
    private readonly IAddPhotoQuery _addPhotoQuery;

    public PhotosController(
        ILogger<PhotosController> logger,
        IGetPhotosByDateDescendingQuery getPhotosByDateDescendingQuery,
        IAddPhotoQuery addPhotoQuery)
    {
        _logger = logger;
        _getPhotosByDateDescendingQuery = getPhotosByDateDescendingQuery;
        _addPhotoQuery = addPhotoQuery;
    }

    [HttpGet]
    public async Task<IReadOnlyCollection<PhotoViewModel>> Get()
    {
        _logger.LogInformation("Call to PhotosController - Get");
        return (await _getPhotosByDateDescendingQuery.Execute()).Select(photo => photo.Map()).ToList();
    }

    [HttpPost]
    public PhotoViewModel AddPhoto(AddPhotoViewModel photo)
    {
        _logger.LogInformation("Call to PhotosController - AddPhoto");
        return _addPhotoQuery.Execute(photo.Map()).Map();
    }
    
}