using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.ViewModels;

namespace PhotographyApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationComponent _authenticationComponent;

        public AuthenticationController(IAuthenticationComponent authenticationComponent)
        {
            _authenticationComponent = authenticationComponent;
        }

        [Authorize(Roles = "PhotographyApi_Admin")]
        [HttpPost]
        public void AddAccount(AccountViewModel accountViewModel) =>
            throw new NotImplementedException();
            //await _authenticationComponent.AddAccount(accountViewModel.UserName, accountViewModel.Password);

        [HttpPost]
        public async Task<string?> VerifyAccount(AccountViewModel accountViewModel)
        {
            return await _authenticationComponent.AuthenticateAccount(accountViewModel.UserName, accountViewModel.Password);
        }
    }
}
