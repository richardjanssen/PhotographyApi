using Business.Entities;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    private readonly string _basePath = "Images";

    public PhotosController(
        IGetPhotosByDateDescendingQuery getPhotosByDateDescendingQuery,
        IAddPhotoQuery addPhotoQuery)
    {
        _getPhotosByDateDescendingQuery = getPhotosByDateDescendingQuery;
        _addPhotoQuery = addPhotoQuery;
    }

    [HttpGet]
    public async Task<IReadOnlyCollection<PhotoViewModel>> Get() =>
        (await _getPhotosByDateDescendingQuery.Execute()).Select(photo => photo.Map(_basePath)).ToList();

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpPost]
    public async Task<PhotoViewModel> UploadPhoto()
    {
        var formCollection = await Request.ReadFormAsync();
        var file = formCollection.Files[0];

        int? albumId = formCollection.ContainsKey("albumId") ? int.Parse(formCollection["albumId"]) : null;
        var addPhoto = new AddPhoto(albumId, file);
        return (await _addPhotoQuery.Execute(addPhoto)).Map(_basePath);
    }
}