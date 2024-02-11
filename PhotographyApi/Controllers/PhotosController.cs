using Business.Entities;
using Business.Interfaces;
using Common.Common;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.Mappers;
using PhotographyApi.ViewModels.Photos;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class PhotosController(
    IAddPhotoQuery addPhotoQuery,
    IPhotographyRepository photographyRepository) : ControllerBase
{
    private const int _homePageAlbumId = 1;

    [HttpGet]
    public async Task<IReadOnlyCollection<PhotoViewModel>> Get() =>
        (await photographyRepository.GetAlbumById(_homePageAlbumId)).Photos
            .Select(photo => photo.Map(Constants.PhotosBasePath))
            .OrderByDescending(photo => photo.Date)
            .ToList();

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpPost]
    public async Task<PhotoViewModel> UploadPhoto()
    {
        var formCollection = await Request.ReadFormAsync();
        var file = formCollection.Files[0];

        if (!formCollection.ContainsKey("albumId"))
        {
            throw new ArgumentException("albumId should be supplied");
        }

        int albumId = int.Parse(formCollection["albumId"]!);
        var addPhoto = new AddPhoto(albumId, file);
        return (await addPhotoQuery.Execute(addPhoto)).Map(Constants.PhotosBasePath);
    }
}