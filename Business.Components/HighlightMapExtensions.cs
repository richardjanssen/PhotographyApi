using Business.Entities;
using Common.Common.Enums;

namespace Business.Components;

public static class HighlightMapExtensions
{
    public static PointWithDistance Map(this IBaseHighlight baseHighlight) => new(
    baseHighlight.Id,
    baseHighlight.Title,
    baseHighlight.Type,
    baseHighlight.Distance);

    public static Highlight Map(this Section section, IReadOnlyCollection<PointWithDistance> children) {
        var sectionHighlight = new SectionHighlight(section.Title, children.Map(), section.StartDistance);
        return new Highlight(HighlightType.Section, sectionHighlight, null);
    }

    public static IReadOnlyCollection<PointHighlight> Map(this IReadOnlyCollection<PointWithDistance> placeHighlights)
    {
        var childrenByDistance = placeHighlights.GroupBy(x => x.Distance);
        return childrenByDistance.Select(group => new PointHighlight(group.Key, group.Any(highlight => highlight.PlaceType == PlaceHighlightType.Location), group.Select(highlight => new Point(highlight.Id, highlight.PlaceType, highlight.Title)).ToList())).ToList();
    }

    public static Highlight Map(this PointHighlight highlight) => new(HighlightType.Place, null, highlight);
}
