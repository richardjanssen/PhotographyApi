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

        return (dbContext.GroceryListProducts.ToList(), dbContext.GroceryListRecurringProducts.ToList());
    }
}
