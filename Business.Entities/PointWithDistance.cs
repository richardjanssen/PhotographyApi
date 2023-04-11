using Common.Common.Enums;

namespace Business.Entities;

public class PointWithDistance
{
    public PointWithDistance(int? id, string title, PlaceHighlightType placeType, double distance)
    {
        Id = id;
        Title = title;
        PlaceType = placeType;
        Distance = distance;
    }

    public int? Id { get; }
    public string Title { get; }
    public PlaceHighlightType PlaceType { get; }
    public double Distance { get; }
}