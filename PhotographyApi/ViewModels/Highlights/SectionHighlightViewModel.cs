namespace PhotographyApi.ViewModels.Highlights;

public class SectionHighlightViewModel
{
    public SectionHighlightViewModel(string title, IReadOnlyCollection<PointHighlightViewModel> children)
    {
        Title = title;
        Children = children;
    }
    public string Title { get; }
    public IReadOnlyCollection<PointHighlightViewModel> Children { get; }
}
