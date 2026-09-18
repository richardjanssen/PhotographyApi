using Business.Entities.Groceries;

namespace Data.Interfaces;

public interface IGroceryRepository
{
    Task<(IReadOnlyCollection<GroceryListProduct> Products, IReadOnlyCollection<GroceryListRecurringProduct> RecurringProducts)> GetGroceries();
}