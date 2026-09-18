using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.ViewModels.Groceries;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class GroceriesController(IGroceryRepository groceryRepository) : ControllerBase
{
    [HttpGet]
    //[Authorize(Roles = ApplicationRoles.Riesj_ShoppingListEdit)]
    public async Task<GroceriesViewModel> Get()
    {
        (var products, var recurringProducts) = await groceryRepository.GetGroceries();

        // Mock data
        return new GroceriesViewModel(
            [
                new GroceryListProductViewModel(1, "Eerste product", false, false),
                new GroceryListProductViewModel(2, "Tweede product", false, false),
                new GroceryListProductViewModel(3, "Derde product (terugkerend)", true, false),
                new GroceryListProductViewModel(4, "Vierde product", false, true),
                new GroceryListProductViewModel(5, "Vijfde product (terugkerend)", true, true),
            ],
            [
                new GroceryListRecurringProductViewModel(1, "Derde product (terugkerend)", 1),
                new GroceryListRecurringProductViewModel(2, "Vijfde product (terugkerend)", 2),
                new GroceryListRecurringProductViewModel(3, "Zesde product (terugkerend)", 3),
                new GroceryListRecurringProductViewModel(4, "Zevende product (terugkerend)", 4),
                ]);


        // Echte data
        //return new GroceriesViewModel([.. products.Select(p => p.Map())], [.. recurringProducts.Select(p => p.Map())]);
    }
}