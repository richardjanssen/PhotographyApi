namespace Common.Common;

public class AppSettings
{
    public string JwtSecret { get; set; } = null!;
    public string JwtIssuer { get; set; } = null!;
    public string RiesjApiKey { get; set; } = null!;
    public string MapboxPublicToken { get; set; } = null!;
    public string GarminExploreRawKmlFeed { get; set; } = null!;
}
