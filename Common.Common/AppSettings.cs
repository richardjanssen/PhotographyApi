namespace Common.Common;

public class AppSettings
{
    public string JwtSecret { get; set; } = null!;
    public string JwtIssuer { get; set; } = null!;
    public int AccessTokenExpirationMinutes { get; set; } = 0;
    public int RefreshTokenExpirationDays { get; set; } = 0;
    public string RiesjApiKey { get; set; } = null!;
    public string MapboxPublicToken { get; set; } = null!;
    public string GarminExploreRawKmlFeed { get; set; } = null!;
}
