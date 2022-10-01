namespace Business.Interfaces;

public interface IAuthenticationComponent
{
    Task<string?> AuthenticateAccount(string userName, string password);
    Task AddAccount(string userName, string password);
}
