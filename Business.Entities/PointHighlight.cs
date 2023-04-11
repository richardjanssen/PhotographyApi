namespace Business.Entities;

public class PointHighlight
{
    public PointHighlight(
        double distance,
        bool currentLocation,
        IReadOnlyCollection<Point> points)
    {
        Distance = distance;
        CurrentLocation = currentLocation;
        Points = points;
    }
    
    public double Distance { get; }
    
    public bool CurrentLocation { get; }

    public IReadOnlyCollection<Point> Points { get; }
}
