namespace PhotographyApi.ViewModels.Accounts;

public record UserDto(long Id, string Username, List<string> Roles);
