namespace Business.Entities.Highlights;

public class SectionHighlight
{
    public SectionHighlight(string title, IReadOnlyCollection<PointHighlight> children, double distance)
    {
        Title = title;
        Children = children;
        StartDistance = distance;
    }

    public string Title { get; }
    public IReadOnlyCollection<PointHighlight> Children { get; }
    public double StartDistance { get; }
}
