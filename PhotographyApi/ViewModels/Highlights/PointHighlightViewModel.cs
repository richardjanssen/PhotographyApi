namespace PhotographyApi.ViewModels.Highlights;

public class PointHighlightViewModel
{
    public PointHighlightViewModel(
        double distance,
        bool currentLocation,
        IReadOnlyCollection<PointViewModel> points)
    {
        Distance = distance;
        CurrentLocation = currentLocation;
        Points = points;
    }

    public double Distance { get; }

    public bool CurrentLocation { get; }

    public IReadOnlyCollection<PointViewModel> Points { get; }
}
