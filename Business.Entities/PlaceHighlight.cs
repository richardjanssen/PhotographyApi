using Common.Common.Enums;

namespace Business.Entities;

public class PlaceHighlight
{
    public PlaceHighlight(
        int? id,
        string title,
        double distance,
        PlaceHighlightType placeType,
        bool currentLocation)
    {
        Id = id;
        Title = title;
        Distance = distance;
        PlaceType = placeType;
        CurrentLocation = currentLocation;
    }

    public int? Id { get; }
    public string Title { get; }
    public double Distance { get; }
    public PlaceHighlightType PlaceType { get; }
    public bool CurrentLocation { get; }
}