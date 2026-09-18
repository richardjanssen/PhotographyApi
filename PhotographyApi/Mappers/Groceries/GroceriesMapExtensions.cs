using Business.Entities.Groceries;
using PhotographyApi.ViewModels.Groceries;

namespace PhotographyApi.Mappers.Groceries;

public static class GroceriesMapExtensions
{
    public static GroceryListProductViewModel Map(this GroceryListProduct product) => new(product.Id, product.Name, product.RecurringProduct, product.Sale);
    public static GroceryListRecurringProductViewModel Map(this GroceryListRecurringProduct product) => new(product.Id, product.Name, product.Order);
}
