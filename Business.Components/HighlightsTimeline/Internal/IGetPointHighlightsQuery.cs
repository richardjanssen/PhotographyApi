using Business.Entities.Highlights;

namespace Business.Components.HighlightsTimeline.Internal;

public interface IGetPointHighlightsQuery
{
    Task<IEnumerable<(PointHighlight Point, int? SectionId)>> Execute();
}
