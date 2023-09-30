using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.ViewModels.Accounts;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationComponent _authenticationComponent;

    public AuthenticationController(IAuthenticationComponent authenticationComponent) =>
        _authenticationComponent = authenticationComponent;

    [HttpPost]
    public async Task<string?> VerifyAccount(AccountViewModel accountViewModel) =>
        await _authenticationComponent.AuthenticateAccount(accountViewModel.UserName, accountViewModel.Password);
}
