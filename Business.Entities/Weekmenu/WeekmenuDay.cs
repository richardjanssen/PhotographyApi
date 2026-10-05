using Business.Entities.Models;

namespace Business.Entities.Weekmenu;

public class WeekmenuDay(Weekday weekday, string name, int order) : EntityBase
{
    public Weekday Weekday { get; private set; } = weekday;
    public string Name { get; private set; } = name;
    public int Order { get; private set; } = order;

    // Parameterless constructor for EF Core
    public WeekmenuDay() : this(Weekday.Monday, string.Empty, 0) { }

    public void Update(Weekday weekday, string name, int order)
    {
        Weekday = weekday;
        Name = name;
        Order = order;
    }
}