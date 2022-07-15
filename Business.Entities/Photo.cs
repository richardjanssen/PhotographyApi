namespace Business.Entities;

public class Photo
{
    public Photo(int? id, DateTime date, IReadOnlyCollection<Image> images)
    {
        Id = id;
        Date = date;
        Images = images;
    }

    public int? Id { get; }
    public DateTime Date { get; }
    public IReadOnlyCollection<Image> Images { get; }
}
