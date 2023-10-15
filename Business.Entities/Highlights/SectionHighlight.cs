namespace Business.Entities.Highlights;

public record SectionHighlight(string Title, IReadOnlyCollection<PointHighlight> Children, double StartDistance, double EndDistance, DateTime? FirstEntered);
