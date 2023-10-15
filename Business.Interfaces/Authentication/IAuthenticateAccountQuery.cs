namespace Business.Interfaces.Authentication;

public interface IAuthenticateAccountQuery
{
    Task<string?> Execute(string userName, string password);
}
