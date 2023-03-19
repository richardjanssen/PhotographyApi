using Common.Common.Enums;

namespace Business.Entities;

public class AddHikerUpdate : IBaseHighlight
{
    public AddHikerUpdate(int? id, string title, PlaceHighlightType type, string? text, double distance, int? albumId)
    {
        Id = id;
        Title = title;
        Type = type;
        Text = text;
        Distance = distance;
        AlbumId = albumId;
    }

    public int? Id { get; }
    public string Title { get; }
    public PlaceHighlightType Type { get; }
    public string? Text { get; }
    public double Distance { get; }
    public int? AlbumId { get; }

}
