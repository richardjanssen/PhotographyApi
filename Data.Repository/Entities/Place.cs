using Common.Common.Enums;

namespace Data.Repository.Entities;

public class Place
{
    public Place(int id, PlaceHighlightType type, string title, double? distance, double lat, double lon)
    {
        Id = id;
        Type = type;
        Title = title;
        Distance = distance;
        Lat = lat;
        Lon = lon;
    }

    public int Id { get; }
    public PlaceHighlightType Type { get; set; }
    public string Title { get; }
    public double? Distance { get; }
    public double Lat { get; set; }
    public double Lon { get; set; }
}
