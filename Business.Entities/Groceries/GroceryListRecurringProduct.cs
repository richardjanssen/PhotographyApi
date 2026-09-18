using Business.Entities.Models;

namespace Business.Entities.Groceries;

public class GroceryListRecurringProduct(string name, int order) : EntityBase
{
    public string Name { get; private set; } = name;
    public int Order { get; private set; } = order;

    // Parameterless constructor for EF Core
    public GroceryListRecurringProduct() : this(string.Empty, 0) { }
}
