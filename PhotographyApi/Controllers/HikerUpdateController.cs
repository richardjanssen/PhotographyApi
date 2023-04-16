using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.Mappers;
using PhotographyApi.ViewModels.HikerUpdates;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class HikerUpdateController : ControllerBase
{
    private readonly IPhotographyRepository _photographyRepository;

    public HikerUpdateController(IPhotographyRepository photographyRepository) =>
        _photographyRepository = photographyRepository;

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpPost]
    public async Task AddHikerUpdate(AddHikerUpdateViewModel addHikerUpdate)
    {
        await _photographyRepository.AddHikerUpdate(addHikerUpdate.Map());
    }
}