using Business.Entities.Users;
using Common.Common;
using Data.Repository.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace Business.Components.Authentication;

public class AuthenticationLogic(RiesjDbContext context, IAccessTokenLogic accessTokenLogic, IOptions<AppSettings> appSettings) : IAuthenticationLogic
{
    public async Task<AuthResponse> Login(string username, string password)
    {
        var users = await context.Users.ToListAsync();

        if (users.Count == 0)
        {
            var salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var hashedPassword = ComputeHash(password, salt);

            // Determine roles for user
            string[] roles = [ApplicationRoles.Riesj_Admin, ApplicationRoles.Riesj_RecipeEdit, ApplicationRoles.Riesj_ShoppingListEdit];
            var userRoles = roles.Select(n => new Role(n)).ToList();

            // Create user
            var newUser = new User(username, hashedPassword, salt, userRoles, []);

            context.Users.Add(newUser);
            await context.SaveChangesAsync();
        }

        var user = await context.Users
            .Include(u => u.Roles)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user == null)
        {
            return new AuthResponse(false, "Invalid username or password");
        }

        var passwordCorrect = VerifyPasswordAgainstHash(password, user.PasswordHash, user.PasswordSalt);
        if (!passwordCorrect)
        {
            return new AuthResponse(false, "Invalid username or password");
        }

        var accessToken = accessTokenLogic.GenerateAccessToken(user);
        var refreshToken = accessTokenLogic.GenerateRefreshToken();

        // Save refresh token
        var refreshTokenEntity = new RefreshToken(refreshToken, DateTime.UtcNow.AddDays(appSettings.Value.RefreshTokenExpirationDays), false, user);
        context.RefreshTokens.Add(refreshTokenEntity);

        await context.SaveChangesAsync();

        return new AuthResponse(true, "Login successful", accessToken, refreshToken, user);
    }

    public async Task<AuthResponse> RefreshToken(string refreshToken)
    {
        var refreshTokenEntity = await context.RefreshTokens
            .Include(rt => rt.User)
            .ThenInclude(u => u.Roles)
            .FirstOrDefaultAsync(rt => rt.Token == refreshToken && !rt.IsRevoked);

        if (refreshTokenEntity == null || refreshTokenEntity.ExpiryDate < DateTime.UtcNow)
        {
            return new AuthResponse(false, "Invalid or expired refresh token");
        }

        var user = refreshTokenEntity.User;
        var accessToken = accessTokenLogic.GenerateAccessToken(user);
        var newRefreshToken = accessTokenLogic.GenerateRefreshToken();

        // Revoke old token and create new one
        refreshTokenEntity.Revoke();

        var newRefreshTokenEntity = new RefreshToken(newRefreshToken, DateTime.UtcNow.AddDays(appSettings.Value.RefreshTokenExpirationDays), false, user);
        context.RefreshTokens.Add(newRefreshTokenEntity);

        await context.SaveChangesAsync();

        return new AuthResponse(true, "Token refreshed successfully", accessToken, newRefreshToken, user);
    }

    public async Task RevokeToken(string refreshToken)
    {
        var refreshTokenEntity = await context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

        if (refreshTokenEntity != null)
        {
            refreshTokenEntity.Revoke();
            await context.SaveChangesAsync();
        }
    }

    public async Task<bool> ValidateRefreshToken(int userId, string refreshToken)
    {
        var token = await context.RefreshTokens
            .FirstOrDefaultAsync(rt =>
                rt.UserId == userId &&
                rt.Token == refreshToken &&
                !rt.IsRevoked &&
                rt.ExpiryDate > DateTime.UtcNow);

        return token != null;
    }

    public async Task CreateUser(string username, string password, string[] roleNames)
    {
        var salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        var hashedPassword = ComputeHash(password, salt);

        // Determine roles for user
        var filteredRoleNames = roleNames.Intersect(ApplicationRoles.AllRoles);
        var userRoles = filteredRoleNames.Select(n => new Role(n)).ToList();

        // Create user
        var user = new User(username, hashedPassword, salt, userRoles, []);

        context.Users.Add(user);
        await context.SaveChangesAsync();
    }

    private static bool VerifyPasswordAgainstHash(string password, string hashedPassword, string salt) =>
        hashedPassword == ComputeHash(password, salt);

    private static string ComputeHash(string password, string salt)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var saltBytes = Encoding.UTF8.GetBytes(salt);
        byte[] derivedKey = Rfc2898DeriveBytes.Pbkdf2(passwordBytes, saltBytes, 10000, HashAlgorithmName.SHA1, 24);

        return Convert.ToBase64String(derivedKey);
    }
}
