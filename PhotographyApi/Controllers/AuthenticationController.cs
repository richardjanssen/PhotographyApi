using Business.Interfaces.Authentication;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.ViewModels.Accounts;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticateAccountQuery _authenticateAccountQuery;

    public AuthenticationController(IAuthenticateAccountQuery authenticateAccountQuery) => _authenticateAccountQuery = authenticateAccountQuery;

    [HttpPost]
    public async Task<string?> VerifyAccount(AccountViewModel accountViewModel)
    {
        // Intentional delay to prevent brute forcing attempts
        await Task.Delay(1000);
        return await _authenticateAccountQuery.Execute(accountViewModel.UserName, accountViewModel.Password);
    }
}
