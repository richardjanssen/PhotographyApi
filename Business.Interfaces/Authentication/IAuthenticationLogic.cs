using Business.Entities.Users;

namespace Business.Components.Authentication;

public interface IAuthenticationLogic
{
    Task<AuthResponse> Login(string username, string password);
    Task<AuthResponse> RefreshToken(string refreshToken);
    Task RevokeToken(string refreshToken);
    Task<bool> ValidateRefreshToken(int userId, string refreshToken);
    Task CreateUser(string username, string password, string[] roleNames);
}