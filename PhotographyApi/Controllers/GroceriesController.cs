using Business.Interfaces.Groceries;
using Common.Common;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.Mappers.Groceries;
using PhotographyApi.ViewModels.Groceries;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class GroceriesController(IGroceryRepository groceryRepository, IAddProductsLogic addProductsLogic) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = ApplicationRoles.Riesj_ShoppingListEdit)]
    public async Task<GroceriesViewModel> Get()
    {
        (var products, var recurringProducts) = await groceryRepository.GetGroceries();


        // Echte data
        return new GroceriesViewModel([.. products.Select(p => p.Map())], [.. recurringProducts.Select(p => p.Map())]);
    }

    [HttpPost]
    [Authorize(Roles = ApplicationRoles.Riesj_ShoppingListEdit)]
    public async Task Save(GroceriesViewModel groceries)
    {
        // Base product order on order of products in request
        var products = groceries.Products.Select((product, i) => product.Map(i + 1)).ToList();

        var recurringProducts = groceries.RecurringProducts.Select(recurringProduct => recurringProduct.Map()).ToList();
        await groceryRepository.UpdateGroceries(products, recurringProducts);
    }

    [HttpPost]
    [Authorize(Roles = ApplicationRoles.Riesj_ShoppingListEdit)]
    public async Task AddProducts(string[] names) => await addProductsLogic.AddProducts(names);
}