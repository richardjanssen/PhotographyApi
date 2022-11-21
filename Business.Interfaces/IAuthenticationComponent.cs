namespace Business.Interfaces;

public interface IAuthenticationComponent
{
    Task<string?> AuthenticateAccount(string userName, string password);
}
