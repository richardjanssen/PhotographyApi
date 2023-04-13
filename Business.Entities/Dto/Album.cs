namespace Business.Entities.Dto;

public class Album
{
    public Album(int id, string title)
    {
        Id = id;
        Title = title;
    }

    public Album(string title)
    {
        Title = title;
    }

    public int Id { get; }
    public string Title { get; }
}
