using Business.Entities.Highlights;
using Common.Common;
using PhotographyApi.ViewModels.Highlights;

namespace PhotographyApi.Mappers;

public static class HighlightMapExtensions
{
    public static HighlightViewModel Map(this Highlight highligt) =>
        new(highligt.Type, highligt.SectionHighlight?.Map(), highligt.PointHighlight?.Map());
    private static SectionHighlightViewModel Map(this SectionHighlight section) =>
        new(section.Title, section.Children.Select((point, pointIndex) => point.Map()).ToList());
    private static PointHighlightViewModel Map(this PointHighlight point) =>
        new(point.Id, point.Date, point.PlaceType, point.Title, point.Distance, point.IsManual, point.Text, point.AlbumDetails?.Map(Constants.PhotosBasePath));
}
