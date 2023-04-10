using Common.Common.Enums;

namespace Business.Entities;

public interface IBaseHighlight
{
    public int? Id { get; }
    public string Title { get; }
    public PlaceHighlightType Type { get; }
    public double Distance { get; }
}
