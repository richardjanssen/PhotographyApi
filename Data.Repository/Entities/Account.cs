namespace Data.Repository.Entities;

public class Account(string userName, string passwordHash, string salt)
{
    public int Id { get; set; }
    public string UserName { get; set; } = userName;
    public string PasswordHash { get; set; } = passwordHash;
    public string Salt { get; set; } = salt;
}
