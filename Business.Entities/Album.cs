namespace Business.Entities;

public class Album
{
    public Album(int? id, string name)
    {
        Id = id;
        Name = name;
    }

    public int? Id { get; set; }
    public string Name { get; }
}
