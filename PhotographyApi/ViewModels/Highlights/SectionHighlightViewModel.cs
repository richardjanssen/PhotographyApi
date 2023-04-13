namespace PhotographyApi.ViewModels.Highlights;

public class SectionHighlightViewModel
{
    public SectionHighlightViewModel(string title, IReadOnlyCollection<PointHighlightViewModel> children, double distance)
    {
        Title = title;
        Children = children;
        StartDistance = distance;
    }

    public string Title { get; }
    public IReadOnlyCollection<PointHighlightViewModel> Children { get; }
    public double StartDistance { get; }
}
