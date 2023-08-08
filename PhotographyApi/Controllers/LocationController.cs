using Business.Interfaces.Locations;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.Mappers;
using PhotographyApi.ViewModels.Locations;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class LocationController : ControllerBase
{
    private readonly IAddManualLocationQuery _addManualLocationQuery;
    private readonly IAddAutomaticLocationQuery _addAutomaticLocationQuery;
    private readonly IGetLocationsQuery _getLocationsQuery;
    private readonly IDeleteLocationQuery _deleteLocationQuery;
    private readonly IPhotographyRepository _photographyRepository;

    public LocationController(
        IAddManualLocationQuery addManualLocationQuery,
        IAddAutomaticLocationQuery addAutomaticLocationQuery,
        IGetLocationsQuery getLocationsQuery,
        IDeleteLocationQuery deleteLocationQuery,
        IPhotographyRepository photographyRepository)
    {
        _addManualLocationQuery = addManualLocationQuery;
        _addAutomaticLocationQuery = addAutomaticLocationQuery;
        _getLocationsQuery = getLocationsQuery;
        _deleteLocationQuery = deleteLocationQuery;
        _photographyRepository = photographyRepository;
    }

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpGet]
    public async Task<IReadOnlyCollection<LocationViewModel>> GetAll() =>
    (await _getLocationsQuery.Execute()).Select(location => location.Map()).ToList();

    [HttpGet]
    public async Task<CoordinateViewModel?> GetCoordinateById(int id) =>
    (await _photographyRepository.GetHikerLocationById(id))?.MapCoordinate();

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpPost]
    public async Task AddManual(ManualLocationViewModel manualLocation) => 
        await _addManualLocationQuery.Execute(manualLocation.PlaceId);

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpPost]
    public async Task AddAutomatic(ManualLocationViewModel manualLocation) =>
        await _addAutomaticLocationQuery.Execute(manualLocation.PlaceId);

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpDelete]
    public async Task Delete(int id) =>
    await _deleteLocationQuery.Execute(id);
}