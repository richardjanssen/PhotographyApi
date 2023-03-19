namespace Data.Repository.Entities;

public class AlbumDetails
{
    public IReadOnlyCollection<Photo> Photos { get; set; } = new List<Photo>();
}
