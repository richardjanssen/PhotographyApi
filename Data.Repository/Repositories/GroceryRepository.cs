using Business.Entities.Groceries;
using Data.Interfaces;
using Data.Repository.Database;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Repositories;

public class GroceryRepository(IDbContextFactory<RiesjDbContext> dbContextFactory) : IGroceryRepository
{
    private readonly IDbContextFactory<RiesjDbContext> _dbContextFactory = dbContextFactory;

    public async Task<(IReadOnlyCollection<GroceryListProduct> Products, IReadOnlyCollection<GroceryListRecurringProduct> RecurringProducts)> GetGroceries()
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync();

        return (dbContext.GroceryListProducts.AsNoTracking().OrderBy(p => p.Order).ToList(), dbContext.GroceryListRecurringProducts.AsNoTracking().OrderBy(rp => rp.Order).ToList());
    }

    public async Task UpdateGroceries(IList<GroceryListProduct> products, IList<GroceryListRecurringProduct> recurringProducts)
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync();
        // Producten verwijderen die wel in DB staan maar niet meer in products
        var productsToDelete = await dbContext.GroceryListProducts.Where(dbProduct => !products.Select(p => p.Id).Contains(dbProduct.Id)).ToListAsync();
        dbContext.GroceryListProducts.RemoveRange(productsToDelete);

        // Producten toevoegen of aanpassen
        for (var i = 0; i < products.Count; i++)
        {
            var product = products[i];
            if (product.Id == 0)
            {
                dbContext.GroceryListProducts.Add(product);
            }
            else
            {
                var dbProduct = dbContext.GroceryListProducts.Single(p => p.Id == product.Id);
                dbProduct.Update(product.Name, product.Order, product.RecurringProduct, product.Sale, product.AlbertHeijn);
            }
        }

        // Recurring producten verwijderen die wel in DB staan maar niet meer in recurringProducts
        //dbRecurringProducts.RemoveAll(dbRecurringProduct => !recurringProducts.Select(p => p.Id).Contains(dbRecurringProduct.Id));
        var recurringProductsToDelete = await dbContext.GroceryListRecurringProducts.Where(dbProduct => !recurringProducts.Select(p => p.Id).Contains(dbProduct.Id)).ToListAsync();
        dbContext.GroceryListRecurringProducts.RemoveRange(recurringProductsToDelete);

        // Recurring producten toevoegen of aanpassen
        for (var i = 0; i < recurringProducts.Count; i++)
        {
            var recurringProduct = recurringProducts[i];
            if (recurringProduct.Id == 0)
            {
                dbContext.GroceryListRecurringProducts.Add(recurringProduct);
            }
            else
            {
                var dbRecurringProduct = dbContext.GroceryListRecurringProducts.Single(p => p.Id == recurringProduct.Id);
                dbRecurringProduct.Update(recurringProduct.Name, recurringProduct.Order);
            }
        }


        await dbContext.SaveChangesAsync();
    }
}
