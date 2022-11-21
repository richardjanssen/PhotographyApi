namespace Data.Repository.Entities;

public class Photo
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public IReadOnlyCollection<Image> Images { get; set; } = new List<Image>();
}
