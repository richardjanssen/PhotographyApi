using Common.Common;

namespace Test.Helpers.Builders.Business.Entities;

public class AppSettingsTestBuilder
{
    private readonly string _jwtIssuer = "SomeJwtIssuer";
    private readonly string _jwtSecret = "SomeVerySecretJwtSecretOfMinimumLength";
    private readonly string _mapboxPublicToken = "SomeFakePublicToken";
    private AppSettingsTestBuilder() { }

    public static AppSettingsTestBuilder ATestBuilder() => new();

    public AppSettings Build() => new() {
        JwtIssuer = _jwtIssuer,
        JwtSecret = _jwtSecret,
        MapboxPublicToken = _mapboxPublicToken
    };
}
