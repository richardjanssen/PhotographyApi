namespace PhotographyApi.ViewModels.Albums;

public class AddAlbumViewModel
{
    public AddAlbumViewModel(string title)
    {
        Title = title;
    }
    public string Title { get; }
}
