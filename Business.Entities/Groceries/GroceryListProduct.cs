using Business.Entities.Models;

namespace Business.Entities.Groceries;

public class GroceryListProduct(string name, int order, bool recurringProduct, bool sale) : EntityBase
{
    public string Name { get; private set; } = name;
    public int Order { get; private set; } = order;
    public bool RecurringProduct { get; private set; } = recurringProduct;
    public bool Sale { get; private set; } = sale;

    // Parameterless constructor for EF Core
    public GroceryListProduct() : this(string.Empty, 0, false, false) { }
}
