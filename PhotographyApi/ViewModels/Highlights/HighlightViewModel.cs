using Common.Common.Enums;

namespace PhotographyApi.ViewModels.Highlights;

public class HighlightViewModel
{
    public HighlightViewModel(
        HighlightType type,
        SectionHighlightViewModel? sectionHighlight,
        PointHighlightViewModel? pointHighlight)
    {
        Type = type;
        SectionHighlight = sectionHighlight;
        PointHighlight = pointHighlight;
    }

    public HighlightType Type { get; }
    public SectionHighlightViewModel? SectionHighlight { get; }
    public PointHighlightViewModel? PointHighlight { get; }
}
