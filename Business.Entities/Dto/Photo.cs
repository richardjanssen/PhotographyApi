namespace Business.Entities.Dto;

public class Photo
{
    public Photo(int id, DateTime date, IReadOnlyCollection<Image> images)
    {
        Id = id;
        Date = date;
        Images = images;
    }

    public Photo(DateTime date, IReadOnlyCollection<Image> images)
    {
        Date = date;
        Images = images;
    }

    public int Id { get; }
    public DateTime Date { get; }
    public IReadOnlyCollection<Image> Images { get; }
}
