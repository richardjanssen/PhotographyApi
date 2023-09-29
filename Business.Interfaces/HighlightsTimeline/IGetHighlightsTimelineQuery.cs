using Business.Entities.Highlights;

namespace Business.Interfaces.HighlightsTimeline;

public interface IGetHighlightsTimelineQuery
{
    Task<IReadOnlyCollection<Highlight>> Execute();
}
