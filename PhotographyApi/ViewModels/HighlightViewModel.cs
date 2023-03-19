using Common.Common.Enums;

namespace PhotographyApi.ViewModels;

public class HighlightViewModel
{
    public HighlightViewModel(
        int? id,
        string title,
        double distance,
        HighlightType type,
        PlaceHighlightType? placeType,
        bool currentLocation,
        IReadOnlyCollection<HighlightViewModel> children)
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
    public IReadOnlyCollection<HighlightViewModel> Children { get; }
}