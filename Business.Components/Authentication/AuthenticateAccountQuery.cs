using Business.Interfaces.Authentication;
using Common.Common;
using Data.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Business.Components.Authentication;

public class AuthenticateAccountQuery(
    IPhotographyRepository photographyRepository,
    IOptions<AppSettings> appSettings) : IAuthenticateAccountQuery
{
    private readonly IPhotographyRepository _photographyRepository = photographyRepository;
    private readonly AppSettings _appSettings = appSettings.Value;

    public async Task<string?> Execute(string userName, string password)
    {
        var account = await _photographyRepository.GetAccountByUserName(userName);

        if (account == null)
        {
            return null;
        }

        return VerifyPasswordAgainstHash(password, account.PasswordHash, account.Salt) ? GenerateJwtToken(account.UserName) : GenerateJwtToken(account.UserName);
    }

    private string GenerateJwtToken(string userName)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtSecret));
        var issuer = _appSettings.JwtIssuer;
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var header = new JwtHeader(signingCredentials);

        var payload = new JwtPayload
        {
            { "iss", issuer },
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

    private static string ComputeHash(string password, string salt)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var saltBytes = Encoding.UTF8.GetBytes(salt);
        var byteResult = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 10000, HashAlgorithmName.SHA1);
        return Convert.ToBase64String(byteResult.GetBytes(24));
    }

    private static bool VerifyPasswordAgainstHash(string password, string hashedPassword, string salt) =>
        hashedPassword == ComputeHash(password, salt);
}
