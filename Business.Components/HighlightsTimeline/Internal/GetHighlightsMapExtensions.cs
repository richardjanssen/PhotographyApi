using Business.Entities.Dto;
using Business.Entities.Highlights;
using Common.Common.Enums;

namespace Business.Components.HighlightsTimeline.Internal;

internal static class GetHighlightsMapExtensions
{
    public static PointHighlight Map(this HikerUpdate update) => new(
        update.Id, update.Date, update.Type, update.Title, update.Distance, true);

    public static Highlight Map(this Section section, IReadOnlyCollection<PointHighlight> children) =>
        new(HighlightType.section, section.MapToSectionHighlight(children), null);

    public static Highlight Map(this PointHighlight highlight) => new(HighlightType.place, null, highlight);

    private static SectionHighlight MapToSectionHighlight(this Section section, IReadOnlyCollection<PointHighlight> children) =>
        new(section.Title, children, section.StartDistance, section.EndDistance);
}
