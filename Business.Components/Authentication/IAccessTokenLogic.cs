using Business.Entities.Users;
using System.Security.Claims;

namespace Business.Components.Authentication;

public interface IAccessTokenLogic
{
    string GenerateRefreshToken();
    string GenerateAccessToken(User user);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}