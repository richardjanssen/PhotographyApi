using Business.Components.Authentication;
using Common.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.Mappers;
using PhotographyApi.ViewModels.Accounts;
using PhotographyApi.ViewModels.Authentication;
using System.Security.Claims;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class AuthenticationController(IAuthenticationLogic authenticationLogic) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        // Intentional delay to prevent brute forcing attempts
        await Task.Delay(1000);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = (await authenticationLogic.Login(request.Username, request.Password)).Map();

        if (!response.Success)
        {
            return Unauthorized(response);
        }

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto request)
    {
        await Task.Delay(1000);

        if (string.IsNullOrEmpty(request.RefreshToken))
        {
            return BadRequest(new { message = "Refresh token is required" });
        }

        var response = (await authenticationLogic.RefreshToken(request.RefreshToken)).Map();

        if (!response.Success)
        {
            return Unauthorized(response);
        }

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (!string.IsNullOrEmpty(refreshToken))
        {
            await authenticationLogic.RevokeToken(refreshToken);
        }

        return Ok(new { message = "Logout successful" });
    }

    [Authorize(Roles = ApplicationRoles.Riesj_Admin)]
    [HttpGet]
    public IActionResult GetProfile()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var username = User.FindFirst(ClaimTypes.Name)?.Value;
        var roles = User.FindAll(ClaimTypes.Role);

        return Ok(new
        {
            userId,
            username,
            roles = roles.Select(r => r.Value)
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateAccountRequestDto request)
    {
        // Intentional delay to prevent brute forcing attempts
        await Task.Delay(1000);
        return Unauthorized();

        //if (request.Password != "TijdelijkWachtwoord")
        //{
        //    return Unauthorized();
        //}

        //await authenticationLogic.CreateUser(request.Username, request.UserPassword, request.RoleNames);
        //return Ok();
    }
}
