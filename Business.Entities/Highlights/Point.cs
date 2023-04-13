using Common.Common.Enums;

namespace Business.Entities.Highlights;

public class Point
{
    public Point(int? id, PlaceHighlightType placeType, string title)
    {
        Id = id;
        PlaceType = placeType;
        Title = title;
    }

    public int? Id { get; }
    public PlaceHighlightType PlaceType { get; }
    public string Title { get; }

}