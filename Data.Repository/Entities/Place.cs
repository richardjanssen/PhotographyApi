using Common.Common.Enums;

namespace Data.Repository.Entities;

public class Place
{
    public Place(int id, int? sectionId, PointHighlightType type, string title, double? distance, double lat, double lon)
    {
        Id = id;
        SectionId = sectionId;
        Type = type;
        Title = title;
        Distance = distance;
        Lat = lat;
        Lon = lon;
    }

    public int Id { get; }
    public int? SectionId { get; }
    public PointHighlightType Type { get; set; }
    public string Title { get; }
    public double? Distance { get; }
    public double Lat { get; set; }
    public double Lon { get; set; }
}
