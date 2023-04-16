using Business.Entities.Highlights;
using PhotographyApi.ViewModels.Highlights;

namespace PhotographyApi.Mappers;

public static class HighlightMapExtensions
{
    public static HighlightViewModel Map(this Highlight highligt, int index) =>
        new(highligt.Type, highligt.SectionHighlight?.Map(index), highligt.PointHighlight?.Map(index));
    private static SectionHighlightViewModel Map(this SectionHighlight section, int sectionIndex) =>
        new(sectionIndex, section.Title, section.Children.Select((point, pointIndex) => point.Map(pointIndex)).ToList());
    private static PointHighlightViewModel Map(this PointHighlight point, int index) =>
        new(index, point.Distance, point.CurrentLocation, point.Points.Select(point => point.Map()).ToList());
    private static PointViewModel Map(this Point point) => new(point.Id, point.PlaceType, point.Title);
}
