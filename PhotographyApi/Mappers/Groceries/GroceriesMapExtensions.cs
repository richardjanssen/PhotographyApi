using Business.Entities.Groceries;
using PhotographyApi.ViewModels.Groceries;

namespace PhotographyApi.Mappers.Groceries;

public static class GroceriesMapExtensions
{
    public static GroceryListProductViewModel Map(this GroceryListProduct product) => new(product.Id, product.RowVersion, product.Name, product.RecurringProduct, product.Sale);
    public static GroceryListRecurringProductViewModel Map(this GroceryListRecurringProduct product) => new(product.Id, product.RowVersion, product.Name, product.Order);
    public static GroceryListProduct Map(this GroceryListProductViewModel productViewModel, int order) => new(productViewModel.Name, order, productViewModel.RecurringProduct, productViewModel.Sale)
    {
        Id = productViewModel.Id ?? 0,
        RowVersion = productViewModel.RowVersion ?? 0
    };

    public static GroceryListRecurringProduct Map(this GroceryListRecurringProductViewModel productViewModel) => new(productViewModel.Name, productViewModel.Order)
    {
        Id = productViewModel.Id ?? 0,
        RowVersion = productViewModel.RowVersion ?? 0
    };
}
