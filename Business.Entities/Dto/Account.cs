namespace Business.Entities.Dto;

public class Account
{
    public Account(string userName, string passwordHash, string salt)
    {
        UserName = userName;
        PasswordHash = passwordHash;
        Salt = salt;
    }

    public string UserName { get; }
    public string PasswordHash { get; }
    public string Salt { get; }
}
