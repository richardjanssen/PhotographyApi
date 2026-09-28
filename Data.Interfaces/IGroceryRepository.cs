using Business.Entities.Groceries;

namespace Data.Interfaces;

public interface IGroceryRepository
{
    Task<(IReadOnlyCollection<GroceryListProduct> Products, IReadOnlyCollection<GroceryListRecurringProduct> RecurringProducts)> GetGroceries();
    Task UpdateGroceries(IList<GroceryListProduct> products, IList<GroceryListRecurringProduct> recurringProducts);

    Task AddProducts(IList<GroceryListProduct> products);
    Task<int> GetCurrentMaximumProductOrder();
}