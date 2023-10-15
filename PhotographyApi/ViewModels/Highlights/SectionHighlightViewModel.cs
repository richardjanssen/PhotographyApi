namespace PhotographyApi.ViewModels.Highlights;

public record SectionHighlightViewModel(
    string Title,
    double StartDistance,
    double EndDistance,
    DateTime? FirstEntered,
    IReadOnlyCollection<PointHighlightViewModel> Children);
