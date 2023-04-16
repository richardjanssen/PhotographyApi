namespace PhotographyApi.ViewModels.Highlights;

public class SectionHighlightViewModel
{
    public SectionHighlightViewModel(int highlightIndex, string title, IReadOnlyCollection<PointHighlightViewModel> children)
    {
        HighlightIndex = highlightIndex;
        Title = title;
        Children = children;
    }

    public int HighlightIndex { get; }
    public string Title { get; }
    public IReadOnlyCollection<PointHighlightViewModel> Children { get; }
}
