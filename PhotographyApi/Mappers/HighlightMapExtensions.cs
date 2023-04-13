using Business.Entities.Highlights;
using PhotographyApi.ViewModels.Highlights;

namespace PhotographyApi.Mappers;

public static class HighlightMapExtensions
{
    public static HighlightViewModel Map(this Highlight highligt) =>
        new(highligt.Type, highligt.SectionHighlight?.Map(), highligt.PointHighlight?.Map());
    private static SectionHighlightViewModel Map(this SectionHighlight section) =>
        new(section.Title, section.Children.Select(point => point.Map()).ToList(), section.StartDistance);
    private static PointHighlightViewModel Map(this PointHighlight point) =>
        new(point.Distance, point.CurrentLocation, point.Points.Select(point => point.Map()).ToList());
    private static PointViewModel Map(this Point point) => new(point.Id, point.PlaceType, point.Title);
}
