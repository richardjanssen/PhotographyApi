using Business.Interfaces.Locations;
using Common.Common;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PhotographyApi.Mappers;
using PhotographyApi.ViewModels.Locations;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class LocationController(
    IAddManualLocationQuery addManualLocationQuery,
    IGetLocationsQuery getLocationsQuery,
    IDeleteLocationQuery deleteLocationQuery,
    IPhotographyRepository photographyRepository,
    IAddSatelliteMessengerLocationQuery addSatelliteMessengerLocationQuery,
    IOptions<AppSettings> appSettings) : ControllerBase
{
    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpGet]
    public async Task<IReadOnlyCollection<LocationViewModel>> GetAll() =>
        (await getLocationsQuery.Execute()).Select(location => location.Map()).ToList();

    [HttpGet]
    public async Task<CoordinateViewModel?> GetCoordinateById(int id) =>
        (await photographyRepository.GetHikerLocationById(id))?.MapCoordinate();

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpPost]
    public async Task AddManual(ManualLocationViewModel manualLocation) => 
        await addManualLocationQuery.Execute(manualLocation.PlaceId);

    [HttpPost]
    public async Task<ActionResult> AddSatelliteMessengerLocation()
    {
        // This flow uses API key authorization as it is called from an external program
        var apiKey = HttpContext.Request.Headers["RiesjApiKey"].ToString();

        if (string.IsNullOrEmpty(apiKey) || apiKey != appSettings.Value.RiesjApiKey)
        {
            return Unauthorized();
        }

        await addSatelliteMessengerLocationQuery.Execute();

        return Ok();
    }

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpDelete]
    public async Task Delete(int id) =>
        await deleteLocationQuery.Execute(id);
}