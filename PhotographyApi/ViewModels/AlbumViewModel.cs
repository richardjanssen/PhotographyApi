namespace PhotographyApi.ViewModels;

public class AlbumViewModel
{
    public AlbumViewModel(int? id, string title)
    {
        Id = id;
        Title = title;
    }

    public int? Id { get; }
    public string Title { get; }
}
