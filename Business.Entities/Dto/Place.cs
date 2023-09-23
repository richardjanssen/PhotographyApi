using Common.Common.Enums;

namespace Business.Entities.Dto;

public class Place
{
    public Place(int id, int? sectionId, PlaceHighlightType type, string title, double? distance, double lat, double lon)
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
    public PlaceHighlightType Type { get; set; }
    public string Title { get; }
    public double? Distance { get; }
    public double Lat { get; }
    public double Lon { get; }
}
