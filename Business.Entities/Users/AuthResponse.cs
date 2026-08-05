namespace Business.Entities.Users;

public record AuthResponse(bool Success, string Message, string? AccessToken, string? RefreshToken, User? User)
{
    public AuthResponse(bool success, string message) : this(success, message, null, null, null) { }
};
