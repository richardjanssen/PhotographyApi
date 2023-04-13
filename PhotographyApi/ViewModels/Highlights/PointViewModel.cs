using Common.Common.Enums;

namespace PhotographyApi.ViewModels.Highlights;

public class PointViewModel
{
    public PointViewModel(int? id, PlaceHighlightType placeType, string title)
    {
        Id = id;
        PlaceType = placeType;
        Title = title;
    }

    public int? Id { get; }
    public PlaceHighlightType PlaceType { get; }
    public string Title { get; }

}