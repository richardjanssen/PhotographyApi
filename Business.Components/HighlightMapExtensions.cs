using Business.Entities;
using Common.Common.Enums;

namespace Business.Components;

public static class HighlightMapExtensions
{
    public static PlaceHighlight Map(this IBaseHighlight baseHighlight, bool isCurrentLocation) => new(
    baseHighlight.Id,
    baseHighlight.Title,
    baseHighlight.Distance,
    baseHighlight.Type,
    isCurrentLocation);

    public static Highlight Map(this Section section, IReadOnlyCollection<PlaceHighlight> children) => new(
        section.Id,
        section.Title,
        section.StartDistance,
        HighlightType.Section,
        null,
        false,
        children.Select(child => child.Map()).ToList());

    public static Highlight Map(this PlaceHighlight highlight) => new(
        highlight.Id,
        highlight.Title,
        highlight.Distance,
        HighlightType.Place,
        highlight.PlaceType,
        highlight.CurrentLocation,
        new List<Highlight>());
}
