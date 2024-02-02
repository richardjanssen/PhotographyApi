using Common.Common.Enums;

namespace PhotographyApi.ViewModels.Highlights;

public record SectionHighlightViewModel(
    string Title,
    SectionHighlightType Type,
    double StartDistance,
    double EndDistance,
    DateTime? FirstEntered,
    IReadOnlyCollection<PointHighlightViewModel> Children);
