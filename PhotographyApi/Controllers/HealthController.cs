using Microsoft.AspNetCore.Mvc;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class HealthController(ILogger<HealthController> logger) : ControllerBase
{
    private readonly ILogger<HealthController> _logger = logger;

    [HttpGet]
    public string Check()
    {
        _logger.LogInformation("Call to HealthController - Check");
        return "Ok";
    }
}