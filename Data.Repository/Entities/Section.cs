using Common.Common.Enums;

namespace Data.Repository.Entities;

public class Section(int id, SectionHighlightType type, string title, double startDistance, double endDistance)
{
    public int Id { get; } = id;
    public SectionHighlightType Type { get; } = type;
    public string Title { get; } = title;
    public double StartDistance { get; } = startDistance;
    public double EndDistance { get; } = endDistance;

}
