using Microsoft.AspNetCore.Mvc;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public string Check() => "Ok!";
}