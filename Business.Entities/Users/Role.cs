using Business.Entities.Models;

namespace Business.Entities.Users;

public class Role : EntityBase
{
    // Parameterless constructor for EF Core
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Role() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public Role(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public long UserId { get; private set; }
}
