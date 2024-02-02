using Common.Common.Enums;

namespace Business.Entities.Highlights;

public record SectionHighlight(string Title, SectionHighlightType Type, IReadOnlyCollection<PointHighlight> Children, double StartDistance, double EndDistance, DateTime? FirstEntered);
