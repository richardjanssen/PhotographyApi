using Data.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.Mappers;
using PhotographyApi.ViewModels;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class AlbumController : ControllerBase
{
    private readonly IPhotographyRepository _photographyRepository;

    public AlbumController(IPhotographyRepository photographyRepository) =>
        _photographyRepository = photographyRepository;

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpPost]
    public async Task AddAlbum(AlbumViewModel album)
    {
        if (album.Id != null)
        {
            throw new ArgumentException("album.Id should be null when creating a new album");
        }

        await _photographyRepository.AddAlbum(album.Map());
    }

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpGet]
    public async Task<IReadOnlyCollection<AlbumViewModel>> GetAll()
    {
        return (await _photographyRepository.GetAlbums()).Select(album => album.Map()).ToList();
    }
}