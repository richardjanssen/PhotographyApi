using Business.Entities.Highlights;

namespace Business.Interfaces.GetHighlights;

public interface IGetHighlightsQuery
{
    Task<IReadOnlyCollection<Highlight>> Execute();
}
