using Business.Entities;

namespace Business.Interfaces;

public interface IGetHighlightsQuery
{
    Task<IReadOnlyCollection<Highlight>> Execute();
}
