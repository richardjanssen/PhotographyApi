using Common.Common.Enums;

namespace PhotographyApi.ViewModels.Highlights;

public class PointViewModel
{
    public PointViewModel(int? id, PointHighlightType placeType, string title)
    {
        Id = id;
        PlaceType = placeType;
        Title = title;
    }

    public int? Id { get; }
    public PointHighlightType PlaceType { get; }
    public string Title { get; }

}