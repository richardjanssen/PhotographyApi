using Business.Entities.Highlights;
using PhotographyApi.ViewModels.Highlights;

namespace PhotographyApi.Mappers;

public static class HighlightMapExtensions
{
    public static HighlightViewModel Map(this Highlight highlight) => new(
        highlight.Type,
        highlight.SectionHighlight?.Map(),
        highlight.PointHighlight?.Map());

    private static SectionHighlightViewModel Map(this SectionHighlight section) => new(
        section.Title,
        section.StartDistance,
        section.EndDistance,
        section.Children.Select((point, pointIndex) => point.Map()).ToList());

    private static PointHighlightViewModel Map(this PointHighlight point) => new(
        point.Id,
        point.Date,
        point.PlaceType,
        point.Title,
        point.Distance,
        point.IsManual);
}
