using Business.Entities.Dto;
using Business.Entities.Highlights;
using Common.Common.Enums;

namespace Business.Components.GetHighlights;

internal static class GetHighlightsMapExtensions
{
    public static PointWithDistance Map(this HikerUpdate update) => new(update.Id, update.Date, update.Title, update.Type, update.Distance, true);

    public static Highlight Map(this Section section, IReadOnlyCollection<PointWithDistance> children) =>
        new(HighlightType.section, section.Map(children.Map()), null);

    public static PointHighlight Map(this PointWithDistance pointWithDistance) => new(
        pointWithDistance.Id,
        pointWithDistance.Date,
        pointWithDistance.PlaceType,
        pointWithDistance.Title,
        pointWithDistance.Distance,
        pointWithDistance.IsManual);

    public static IReadOnlyCollection<PointHighlight> Map(this IReadOnlyCollection<PointWithDistance> placeHighlights) =>
        placeHighlights.Select(placeHighlight => placeHighlight.Map()).ToList();

    public static Highlight Map(this PointHighlight highlight) => new(HighlightType.place, null, highlight);

    private static SectionHighlight Map(this Section section, IReadOnlyCollection<PointHighlight> children) =>
        new(section.Title, children, section.StartDistance);
}
