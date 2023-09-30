namespace PhotographyApi.ViewModels.Highlights;

public record SectionHighlightViewModel(string Title, double StartDistance, double EndDistance, IReadOnlyCollection<PointHighlightViewModel> Children);
