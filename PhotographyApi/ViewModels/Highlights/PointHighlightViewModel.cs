namespace PhotographyApi.ViewModels.Highlights;

public class PointHighlightViewModel
{
    public PointHighlightViewModel(
        int highlightIndex,
        double distance,
        bool currentLocation,
        IReadOnlyCollection<PointViewModel> points)
    {
        HighlightIndex = highlightIndex;
        Distance = distance;
        CurrentLocation = currentLocation;
        Points = points;
    }

    public int HighlightIndex { get; }
    public double Distance { get; }
    public bool CurrentLocation { get; }
    public IReadOnlyCollection<PointViewModel> Points { get; }
}
