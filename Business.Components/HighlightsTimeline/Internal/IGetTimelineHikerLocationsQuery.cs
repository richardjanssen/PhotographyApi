using Business.Entities.Dto;
using Business.Entities.Highlights;

namespace Business.Components.HighlightsTimeline.Internal;

public interface IGetTimelineHikerLocationsQuery
{
    Task<IEnumerable<(PointHighlight Point, int? SectionId)>> Execute(IReadOnlyCollection<Place> places);
}
