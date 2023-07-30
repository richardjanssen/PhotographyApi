using Business.Entities.Dto;
using Common.Common.Enums;

namespace Business.Entities.Highlights;

public class PointHighlight
{
    public PointHighlight(int id, DateTime date, PlaceHighlightType placeType, string title, double distance, bool isManual, string? text, AlbumDetails? albumDetails)
    {
        Id = id;
        Date = date;
        PlaceType = placeType;
        Title = title;
        Distance = distance;
        IsManual = isManual;
        Text = text;
        AlbumDetails = albumDetails;
    }

    public int Id { get; }
    public DateTime Date { get; }
    public PlaceHighlightType PlaceType { get; }
    public string Title { get; }
    public double Distance { get; }
    public bool IsManual { get; }
    public string? Text { get; }
    public AlbumDetails? AlbumDetails { get; }
}
