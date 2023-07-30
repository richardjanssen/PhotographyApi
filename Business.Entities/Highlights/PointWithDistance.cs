using Common.Common.Enums;

namespace Business.Entities.Highlights;

public class PointWithDistance
{
    public PointWithDistance(int id, DateTime date, string title, PlaceHighlightType placeType, double distance, bool isManual)
    {
        Id = id;
        Date = date;
        Title = title;
        PlaceType = placeType;
        Distance = distance;
        IsManual = isManual;
    }

    public int Id { get; }
    public DateTime Date { get; }
    public string Title { get; }
    public PlaceHighlightType PlaceType { get; }
    public double Distance { get; }
    public bool IsManual { get; }
}