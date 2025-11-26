using Business.Interfaces.Authentication;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.ViewModels.Accounts;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class AuthenticationController(IAuthenticateAccountQuery authenticateAccountQuery) : ControllerBase
{
    [HttpPost]
    public async Task<string?> VerifyAccount(AccountViewModel accountViewModel)
    {
        // Intentional delay to prevent brute forcing attempts
        await Task.Delay(1000);
        return await authenticateAccountQuery.Execute(accountViewModel.UserName, accountViewModel.Password);
    }
}
