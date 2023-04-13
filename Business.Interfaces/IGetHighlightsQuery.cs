using Business.Entities.Highlights;

namespace Business.Interfaces;

public interface IGetHighlightsQuery
{
    Task<IReadOnlyCollection<Highlight>> Execute();
}
