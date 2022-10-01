using Business.Entities;
using Business.Interfaces;
using Common.Common;
using Data.Repository.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Business.Components;

public class AuthenticationComponent : IAuthenticationComponent
{
    private readonly IPhotographyRepository _photographyRepository;
    private readonly AppSettings _appSettings;

    public AuthenticationComponent(
        IPhotographyRepository photographyRepository, 
        IOptions<AppSettings> appSettings)
    {
        _photographyRepository = photographyRepository;
        _appSettings = appSettings.Value;
    }

    public async Task<string?> AuthenticateAccount(string userName, string password)
    {
        var account = await _photographyRepository.GetAccountByUserName(userName);

        if (account == null)
        {
            return null;
        }

        return VerifyPasswordAgainstHash(password, account.PasswordHash, account.Salt) ? GenerateJwtToken(account.UserName) : null;
    }

    public async Task AddAccount(string userName, string password)
    {
        var salt = GenerateSalt();
        var hashedPassword = ComputeHash(password, salt);

        await _photographyRepository.AddAccount(new Account(userName, hashedPassword, salt));
    }

    private string GenerateJwtToken(string userName)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtSecret));
        var issuer = _appSettings.JwtIssuer;
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var header = new JwtHeader(signingCredentials);

        var payload = new JwtPayload
        {
            { "iss", issuer},
            { "aud", issuer },
            { "iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds()},
            { "exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds()},
            { "role", new List<string>(){ "PhotographyApi_Admin" } },
            { "name", userName }
        };

        var securityToken = new JwtSecurityToken(header, payload);
        var handler = new JwtSecurityTokenHandler();

        return handler.WriteToken(securityToken);
    }

    private static string GenerateSalt()
    {
        var salt = new byte[128 / 8];
        using var generator = RandomNumberGenerator.Create();
        generator.GetBytes(salt);
        return Convert.ToBase64String(salt);
    }

    private static string ComputeHash(string password, string salt)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var saltBytes = Encoding.UTF8.GetBytes(salt);
        var byteResult = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 10000);
        return Convert.ToBase64String(byteResult.GetBytes(24));
    }

    private static bool VerifyPasswordAgainstHash(string password, string hashedPassword, string salt) => 
        hashedPassword == ComputeHash(password, salt);
}
