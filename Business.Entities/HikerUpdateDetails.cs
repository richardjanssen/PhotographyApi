using Business.Entities.Dto;
using Common.Common.Enums;

namespace Business.Entities;

public class HikerUpdateDetails
{
    public HikerUpdateDetails(int id, string title, PlaceHighlightType type, string text, double distance, Album album)
    {
        Id = id;
        Title = title;
        Type = type;
        Text = text;
        Distance = distance;
        Album = album;
    }

    public int? Id { get; }
    public string Title { get; }
    public PlaceHighlightType Type { get; }
    public string? Text { get; }
    public double Distance { get; }
    public Album Album { get; }

}
