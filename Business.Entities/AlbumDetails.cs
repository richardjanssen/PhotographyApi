namespace Business.Entities;

public class AlbumDetails
{
    public AlbumDetails(IReadOnlyCollection<Photo> photos)
    {
        Photos = photos;
    }

    public IReadOnlyCollection<Photo> Photos { get; set; } = new List<Photo>();
}
