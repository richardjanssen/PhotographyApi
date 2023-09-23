using Common.Common.Enums;

namespace Business.Entities.Highlights;

public class PointWithDistance
{
    public PointWithDistance(int id, int? sectionId, DateTime date, string title, PlaceHighlightType placeType, double? distance, bool isManual)
    {
        Id = id;
        SectionId = sectionId;
        Date = date;
        Title = title;
        PlaceType = placeType;
        Distance = distance;
        IsManual = isManual;
    }

    public int Id { get; }
    public int? SectionId { get; }
    public DateTime Date { get; }
    public string Title { get; }
    public PlaceHighlightType PlaceType { get; }
    public double? Distance { get; }
    public bool IsManual { get; }
}