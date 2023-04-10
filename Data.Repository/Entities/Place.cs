using Common.Common.Enums;

namespace Data.Repository.Entities;

public class Place
{
    public Place(int id, PlaceHighlightType type, string title, double distance)
    {
        Id = id;
        Type = type;
        Title = title;
        Distance = distance;
    }

    public int Id { get; }
    public PlaceHighlightType Type { get; set; }
    public string Title { get; }
    public double Distance { get; }
}
