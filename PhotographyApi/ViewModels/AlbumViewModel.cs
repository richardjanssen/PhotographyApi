namespace PhotographyApi.ViewModels;

public class AlbumViewModel
{
    public AlbumViewModel(int? id, string name)
    {
        Id = id;
        Name = name;
    }

    public int? Id { get; set; }
    public string Name { get; set; }
}
