namespace PhotographyApi.ViewModels.Authentication;

public record CreateAccountRequestDto(string Password, string Username, string UserPassword, string[] RoleNames);
