using Common.Common.Enums;

namespace PhotographyApi.ViewModels.HikerUpdates;

public class AddHikerUpdateViewModel
{
    public AddHikerUpdateViewModel(string title, PlaceHighlightType type, string? text, double distance, int? albumId)
    {
        Title = title;
        Type = type;
        Text = text;
        Distance = distance;
        AlbumId = albumId;
    }

    public string Title { get; }
    public PlaceHighlightType Type { get; }
    public string? Text { get; }
    public double Distance { get; }
    public int? AlbumId { get; }
}