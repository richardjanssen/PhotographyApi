namespace PhotographyApi.ViewModels.Accounts;

public record AuthResponseDto(bool Success, string Message, string? AccessToken, string? RefreshToken, UserDto? User);
