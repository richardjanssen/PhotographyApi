using Common.Common.Enums;

namespace Data.Repository.Entities;

public class Place(int id, int? sectionId, PointHighlightType type, string title, double? distance, double lat, double lon)
{
    public int Id { get; } = id;
    public int? SectionId { get; } = sectionId;
    public PointHighlightType Type { get; set; } = type;
    public string Title { get; } = title;
    public double? Distance { get; } = distance;
    public double Lat { get; set; } = lat;
    public double Lon { get; set; } = lon;
}
