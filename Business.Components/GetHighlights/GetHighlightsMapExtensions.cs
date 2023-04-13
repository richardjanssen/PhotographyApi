using Business.Entities.Dto;
using Business.Entities.Highlights;
using Common.Common.Enums;

namespace Business.Components.GetHighlights;

public static class GetHighlightsMapExtensions
{
    public static PointWithDistance Map(this Place place) => new(place.Id, place.Title, place.Type, place.Distance);

    public static PointWithDistance Map(this HikerUpdate update) => new(update.Id, update.Title, update.Type, update.Distance);

    public static Highlight Map(this Section section, IReadOnlyCollection<PointWithDistance> children) =>
        new(HighlightType.Section, section.Map(children.Map()), null);

    public static IReadOnlyCollection<PointHighlight> Map(this IReadOnlyCollection<PointWithDistance> placeHighlights)
    {
        var childrenByDistance = placeHighlights.GroupBy(x => x.Distance);
        return childrenByDistance
            .Select(group => new PointHighlight(
                group.Key,
                group.Any(highlight => highlight.PlaceType == PlaceHighlightType.Location),
                group.Select(highlight => new Point(highlight.Id, highlight.PlaceType, highlight.Title)).ToList()))
            .ToList();
    }

    public static Highlight Map(this PointHighlight highlight) => new(HighlightType.Place, null, highlight);

    private static SectionHighlight Map(this Section section, IReadOnlyCollection<PointHighlight> children) =>
        new(section.Title, children, section.StartDistance);
}
