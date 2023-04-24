using PhotographyApi.ViewModels.Albums;

namespace PhotographyApi.ViewModels.HikerUpdates;

public class HikerUpdateDetailsViewModel
{
    public HikerUpdateDetailsViewModel(string? text, AlbumDetailsViewModel? album)
    {
        Text = text;
        Album = album;
    }
    public string? Text { get; }
    public AlbumDetailsViewModel? Album { get; }

}
