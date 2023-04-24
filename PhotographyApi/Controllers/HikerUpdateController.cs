using Business.Interfaces;
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
    private readonly IGetHikerUpdateDetailsQuery _getHikerUpdateDetailsQuery;
    private readonly IPhotographyRepository _photographyRepository;

    public HikerUpdateController(IGetHikerUpdateDetailsQuery getHikerUpdateDetailsQuery, IPhotographyRepository photographyRepository)
    {
        _getHikerUpdateDetailsQuery = getHikerUpdateDetailsQuery;
        _photographyRepository = photographyRepository;
    }

    [HttpGet]
    public async Task<HikerUpdateDetailsViewModel> Get(int id) =>
        (await _getHikerUpdateDetailsQuery.Execute(id)).Map();

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpPost]
    public async Task AddHikerUpdate(AddHikerUpdateViewModel addHikerUpdate)
    {
        await _photographyRepository.AddHikerUpdate(addHikerUpdate.Map());
    }
}