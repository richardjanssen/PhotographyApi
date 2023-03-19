using Data.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.Mappers;
using PhotographyApi.ViewModels;

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
        if (addHikerUpdate.Id != null)
        {
            throw new ArgumentException("addHikerUpdate.Id should be null when creating a new hiker update");
        }

        await _photographyRepository.AddHikerUpdate(addHikerUpdate.Map());
    }
}