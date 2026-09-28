using Business.Interfaces.Groceries;
using Data.Interfaces;

namespace Business.Components.Groceries;

public class AddProductsLogic(IGroceryRepository groceryRepository) : IAddProductsLogic
{
    public async Task AddProducts(string[] names)
    {
        var maxOrder = await groceryRepository.GetCurrentMaximumProductOrder();

        var products = names
            .Select((name, index) => new Entities.Groceries.GroceryListProduct(name, maxOrder + index + 1, false, false, false))
            .ToList();

        await groceryRepository.AddProducts(products);
    }
}
