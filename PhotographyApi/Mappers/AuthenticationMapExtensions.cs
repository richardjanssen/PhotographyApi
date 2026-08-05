using Business.Entities.Users;
using PhotographyApi.ViewModels.Accounts;

namespace PhotographyApi.Mappers;

public static class AuthenticationMapExtensions
{
    public static AuthResponseDto Map(this AuthResponse authResponse) =>
        new(authResponse.Success, authResponse.Message, authResponse.AccessToken, authResponse.RefreshToken, authResponse.User?.Map());

    private static UserDto Map(this User user) =>
        new(user.Id, user.Username, [.. user.Roles.Select(r => r.Name)]);
}
