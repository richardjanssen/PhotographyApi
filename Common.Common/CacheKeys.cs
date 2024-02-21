namespace Common.Common;

public static class CacheKeys
{
    public const string Highlights = "Highlights";
    private const string _mapLocations = "MapLocations";

    public static string GetMapLocationCacheKey(int locationId)
    {
        return $"{_mapLocations}{locationId}";
    }
}
