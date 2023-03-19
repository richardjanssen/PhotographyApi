using Business.Entities;
using Common.Common.Enums;

namespace Business.Components;

public static class HighlightMapExtensions
{
    public static Highlight Map(this IBaseHighlight baseHighlight, HighlightType type) => new(
        baseHighlight.Id,
        baseHighlight.Title,
        baseHighlight.Distance,
        type,
        baseHighlight.Type,
        false,
        new List<Highlight>());
}
