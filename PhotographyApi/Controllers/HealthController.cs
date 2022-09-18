using Microsoft.AspNetCore.Mvc;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class HealthController : ControllerBase
{
    private readonly ILogger<HealthController> _logger;

    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public string Check()
    {
        _logger.LogInformation("Call to HealthController - Check");
        return "Ok!";
    }
}