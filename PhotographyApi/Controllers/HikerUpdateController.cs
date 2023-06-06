using Business.Components.GetHighlights;
using Business.Interfaces.HikerUpdates;
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
    private readonly IGetHikerUpdatesQuery _getHikerUpdatesQuery;
    private readonly IDeleteHikerUpdateQuery _deleteHikerUpdateQuery;

    public HikerUpdateController(
        IGetHikerUpdateDetailsQuery getHikerUpdateDetailsQuery,
        IPhotographyRepository photographyRepository,
        IGetHikerUpdatesQuery getHikerUpdatesQuery,
        IDeleteHikerUpdateQuery deleteHikerUpdateQuery)
    {
        _getHikerUpdateDetailsQuery = getHikerUpdateDetailsQuery;
        _photographyRepository = photographyRepository;
        _getHikerUpdatesQuery = getHikerUpdatesQuery;
        _deleteHikerUpdateQuery = deleteHikerUpdateQuery;
    }

    [HttpGet]
    public async Task<HikerUpdateDetailsViewModel> GetById(int id) =>
        (await _getHikerUpdateDetailsQuery.Execute(id)).Map();

    //[Authorize(Roles = "PhotographyApi_Admin")]
    [HttpGet]
    public async Task<IReadOnlyCollection<HikerUpdateBasicViewModel>> GetAll() =>
        (await _getHikerUpdatesQuery.Execute()).Select(update => update.Map()).ToList();

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpPost]
    public async Task AddHikerUpdate(HikerUpdateViewModel addHikerUpdate)
    {
        await _photographyRepository.AddHikerUpdate(addHikerUpdate.Map());
    }

    //[Authorize(Roles = "PhotographyApi_Admin")]
    [HttpDelete]
    public async Task Delete(int id) => await _deleteHikerUpdateQuery.Execute(id);
}