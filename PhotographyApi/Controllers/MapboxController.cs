using Common.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace PhotographyApi.Controllers;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class MapboxController : ControllerBase
{
    private readonly IOptions<AppSettings> _appSettings;

    public MapboxController(IOptions<AppSettings> appSettings) => _appSettings = appSettings;

    [HttpGet]
    public string GetPublicToken(string application)
    {
        // Even though this is a public token, we add a very simple check to prevent bots from obtaining this token
        if (application != "riesj")
        {
            throw new Exception("Something went wrong while retrieving the mapbox public token.");
        }

        return _appSettings.Value.MapboxPublicToken;
    }
}
