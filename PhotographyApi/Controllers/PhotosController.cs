using System.Net.Http.Headers;
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
    private readonly IWebHostEnvironment _env;

    public PhotosController(
        ILogger<PhotosController> logger,
        IGetPhotosByDateDescendingQuery getPhotosByDateDescendingQuery,
        IAddPhotoQuery addPhotoQuery,
        IWebHostEnvironment env)
    {
        _logger = logger;
        _getPhotosByDateDescendingQuery = getPhotosByDateDescendingQuery;
        _addPhotoQuery = addPhotoQuery;
        _env = env;
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
        // Commented out until authorisation
        //_logger.LogInformation("Call to PhotosController - AddPhoto");
        //return _addPhotoQuery.Execute(photo.Map()).Map();

        throw new NotImplementedException("Not implemented until authorisation");
    }

    [HttpPost]
    public string UploadPhoto()
    {
        // Commented out until authorisation
        //var formCollection = await Request.ReadFormAsync();
        //var file = formCollection.Files[0];
        //var folderName = "Images";
        //var pathToSave = Path.Combine(_env.WebRootPath, folderName);
        //if (file.Length > 0)
        //{
        //    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName?.Trim('"') ?? throw new Exception("Expected a FileName");
        //    var fullPath = Path.Combine(pathToSave, fileName);
        //    var dbPath = Path.Combine(folderName, fileName);
        //    using (var stream = new FileStream(fullPath, FileMode.Create))
        //    {
        //        file.CopyTo(stream);
        //    }
        //    return dbPath;
        //}

        //throw new Exception("No file");

        throw new NotImplementedException("Not implemented until authorisation");
    }
}