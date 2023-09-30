using Business.Interfaces.HikerUpdates;
using Common.Common.Interfaces;
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
    private readonly IDateTimeProvider _dateTimeProvider;

    public HikerUpdateController(
        IGetHikerUpdateDetailsQuery getHikerUpdateDetailsQuery,
        IPhotographyRepository photographyRepository,
        IGetHikerUpdatesQuery getHikerUpdatesQuery,
        IDeleteHikerUpdateQuery deleteHikerUpdateQuery,
        IDateTimeProvider dateTimeProvider)
    {
        _getHikerUpdateDetailsQuery = getHikerUpdateDetailsQuery;
        _photographyRepository = photographyRepository;
        _getHikerUpdatesQuery = getHikerUpdatesQuery;
        _deleteHikerUpdateQuery = deleteHikerUpdateQuery;
        _dateTimeProvider = dateTimeProvider;
    }

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpGet]
    public async Task<IReadOnlyCollection<HikerUpdateBasicViewModel>> GetAll() =>
        (await _getHikerUpdatesQuery.Execute()).Select(update => update.MapToHikerUpdateBasic()).ToList();

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpGet]
    public async Task<AddHikerUpdateViewModel?> GetById(int id) =>
    (await _photographyRepository.GetHikerUpdateById(id))?.Map();

    [HttpGet]
    public async Task<HikerUpdateDetailsViewModel> GetDetailsById(int id) =>
        (await _getHikerUpdateDetailsQuery.Execute(id)).Map();

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpPost]
    public async Task Add(AddHikerUpdateViewModel addHikerUpdate) => await _photographyRepository.AddHikerUpdate(addHikerUpdate.Map(_dateTimeProvider.UtcNow));

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpPut]
    public async Task Update(AddHikerUpdateViewModel addHikerUpdate) => await _photographyRepository.UpdateHikerUpdate(
        addHikerUpdate.Map(addHikerUpdate.Id ?? throw new InvalidOperationException("Id should always have a value when updating hiker update"),
        _dateTimeProvider.UtcNow));


    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpDelete]
    public async Task Delete(int id) => await _deleteHikerUpdateQuery.Execute(id);
}