using Business.Interfaces.Locations;
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
    private readonly IGetLocationsQuery _getLocationsQuery;
    private readonly IDeleteLocationQuery _deleteLocationQuery;

    public LocationController(
        IAddManualLocationQuery addManualLocationQuery,
        IGetLocationsQuery getLocationsQuery,
        IDeleteLocationQuery deleteLocationQuery)
    {
        _addManualLocationQuery = addManualLocationQuery;
        _getLocationsQuery = getLocationsQuery;
        _deleteLocationQuery = deleteLocationQuery;
    }

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpGet]
    public async Task<IReadOnlyCollection<LocationViewModel>> GetAll() =>
    (await _getLocationsQuery.Execute()).Select(location => location.Map()).ToList();

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpPost]
    public async Task AddManual(ManualLocationViewModel manualLocation) => 
        await _addManualLocationQuery.Execute(manualLocation.Distance);

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpDelete]
    public async Task Delete(int id) =>
    await _deleteLocationQuery.Execute(id);
}