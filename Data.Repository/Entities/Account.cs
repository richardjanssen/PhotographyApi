using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Repository.Entities;

[Table("Accounts")]
public class Account
{
    public Account(string userName, string passwordHash, string salt)
    {
        UserName = userName;
        PasswordHash = passwordHash;
        Salt = salt;
    }

    public int Id { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
}
