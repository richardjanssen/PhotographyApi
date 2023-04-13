using Common.Common.Enums;

namespace Business.Entities.Highlights;

public class Highlight
{
    public Highlight(HighlightType type, SectionHighlight? sectionHighlight, PointHighlight? pointHighlight)
    {
        Type = type;
        SectionHighlight = sectionHighlight;
        PointHighlight = pointHighlight;
    }

    public HighlightType Type { get; }
    public SectionHighlight? SectionHighlight { get; }
    public PointHighlight? PointHighlight { get; }
}
