using Business.Entities.Dto;

namespace Business.Entities;

public class HikerUpdateDetails
{
    public HikerUpdateDetails(string? text, AlbumDetails? album)
    {
        Text = text;
        Album = album;
    }
    public string? Text { get; }
    public AlbumDetails? Album { get; }

}
