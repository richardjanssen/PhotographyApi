using Business.Interfaces.Locations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.ViewModels.Locations;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class LocationController : ControllerBase
{
    private readonly IAddManualLocationQuery _addManualLocationQuery;

    public LocationController(IAddManualLocationQuery addManualLocationQuery) =>
        _addManualLocationQuery = addManualLocationQuery;

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpPost]
    public async Task AddManual(ManualLocationViewModel manualLocation) =>
        await _addManualLocationQuery.Execute(manualLocation.Distance);
}