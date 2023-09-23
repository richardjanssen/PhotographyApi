using Common.Common.Enums;

namespace Business.Entities.Dto;

public class HikerUpdate
{
    public HikerUpdate(int id, DateTime date, string title, PlaceHighlightType type, string? text, double distance, int? albumId, int? placeId)
    {
        Id = id;
        Date = date;
        Title = title;
        Type = type;
        Text = text;
        Distance = distance;
        AlbumId = albumId;
        PlaceId = placeId;
    }

    public HikerUpdate(DateTime date, string title, PlaceHighlightType type, string? text, double distance, int? albumId, int? placeId)
    {
        Date = date;
        Title = title;
        Type = type;
        Text = text;
        Distance = distance;
        AlbumId = albumId;
        PlaceId = placeId;
    }

    public int Id { get; }
    public DateTime Date { get; }
    public string Title { get; }
    public PlaceHighlightType Type { get; }
    public string? Text { get; }
    public double Distance { get; }
    public int? AlbumId { get; }
    public int? PlaceId { get; }

}
