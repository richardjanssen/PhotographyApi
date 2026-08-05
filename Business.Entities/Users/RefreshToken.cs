using Business.Entities.Models;

namespace Business.Entities.Users;

public class RefreshToken : EntityBase
{
    // Parameterless constructor for EF Core
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public RefreshToken() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public RefreshToken(string token, DateTime expiryDate, bool isRevoked, User user)
    {
        Token = token;
        ExpiryDate = expiryDate;
        IsRevoked = isRevoked;
        User = user;
    }

    public string Token { get; private set; }
    public DateTime ExpiryDate { get; private set; }
    public bool IsRevoked { get; private set; }
    public User User { get; private set; }
    public long UserId { get; private set; }

    public void Revoke()
    {
        IsRevoked = true;
    }
}