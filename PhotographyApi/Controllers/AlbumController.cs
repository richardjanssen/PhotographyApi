using Common.Common;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.Mappers;
using PhotographyApi.ViewModels;
using PhotographyApi.ViewModels.Albums;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class AlbumController(IPhotographyRepository photographyRepository) : ControllerBase
{
    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpPost]
    public async Task AddAlbum(AddAlbumViewModel album)
    {
        await photographyRepository.AddAlbum(album.Map());
    }

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpGet]
    public async Task<IReadOnlyCollection<AlbumViewModel>> GetAll()
    {
        return (await photographyRepository.GetAlbums()).Select(album => album.Map()).ToList();
    }

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpGet]
    public async Task<AlbumDetailsViewModel> GetById(int id)
    {
        return (await photographyRepository.GetAlbumById(id)).Map(Constants.PhotosBasePath);
    }

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpDelete]
    public async Task DeletePhoto(DeletePhotoViewModel deletePhoto)
    {
        await photographyRepository.DeleteAlbumPhoto(deletePhoto.AlbumId, deletePhoto.PhotoId);
    }
}