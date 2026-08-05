using Business.Entities.Models;

namespace Business.Entities.Users;

public class User : EntityBase
{
    // Parameterless constructor for EF Core
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public User() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public User(string username, string passwordHash, string passwordSalt, List<Role> roles, List<RefreshToken> refreshTokens)
    {
        Username = username;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        Roles = roles;
        RefreshTokens = refreshTokens;
    }

    public string Username { get; private set; }
    public string PasswordHash { get; private set; }
    public string PasswordSalt { get; private set; }
    public List<Role> Roles { get; private set; }
    public List<RefreshToken> RefreshTokens { get; private set; }
}