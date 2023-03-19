using Common.Common.Enums;

namespace Business.Entities;

public class Highlight
{
    public Highlight(
        int? id,
        string title,
        double distance,
        HighlightType type,
        PlaceHighlightType? placeType,
        bool currentLocation,
        IReadOnlyCollection<Highlight> children)
    {
        Id = id;
        Title = title;
        Distance = distance;
        Type = type;
        PlaceType = placeType;
        CurrentLocation = currentLocation;
        Children = children;
    }

    public int? Id { get; }
    public string Title { get; }
    public double Distance { get; }
    public HighlightType Type { get; }
    public PlaceHighlightType? PlaceType { get; }
    public bool CurrentLocation { get; }
    public IReadOnlyCollection<Highlight> Children { get; }
}