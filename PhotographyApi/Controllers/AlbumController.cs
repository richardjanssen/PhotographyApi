using Common.Common;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.Mappers;
using PhotographyApi.ViewModels;
using PhotographyApi.ViewModels.Albums;

namespace PhotographyApi.Controllers;
//Bla bla bla bla
[ApiController]
[Route("api/v1/[controller]/[action]")]
public class AlbumController : ControllerBase
{
    private readonly IPhotographyRepository _photographyRepository;

    public AlbumController(IPhotographyRepository photographyRepository) =>
        _photographyRepository = photographyRepository;

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpPost]
    public async Task AddAlbum(AddAlbumViewModel album)
    {
        await _photographyRepository.AddAlbum(album.Map());
    }

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpGet]
    public async Task<IReadOnlyCollection<AlbumViewModel>> GetAll()
    {
        return (await _photographyRepository.GetAlbums()).Select(album => album.Map()).ToList();
    }

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpGet]
    public async Task<AlbumDetailsViewModel> GetById(int id)
    {
        return (await _photographyRepository.GetAlbumById(id)).Map(Constants.PhotosBasePath);
    }
}