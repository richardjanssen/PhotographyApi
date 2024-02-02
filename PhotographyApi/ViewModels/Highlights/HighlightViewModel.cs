using Common.Common.Enums;

namespace PhotographyApi.ViewModels.Highlights;

public class HighlightViewModel(
    HighlightType type,
    SectionHighlightViewModel? sectionHighlight,
    PointHighlightViewModel? pointHighlight)
{
    public HighlightType Type { get; } = type;
    public SectionHighlightViewModel? SectionHighlight { get; } = sectionHighlight;
    public PointHighlightViewModel? PointHighlight { get; } = pointHighlight;
}
