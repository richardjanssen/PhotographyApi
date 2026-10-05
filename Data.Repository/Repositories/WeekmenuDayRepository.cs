using Business.Entities.Weekmenu;
using Data.Interfaces;
using Data.Repository.Database;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Repositories;

public class WeekmenuDayRepository(IDbContextFactory<RiesjDbContext> dbContextFactory) : IWeekmenuDayRepository
{
    private readonly IDbContextFactory<RiesjDbContext> _dbContextFactory = dbContextFactory;
    public async Task<IReadOnlyCollection<WeekmenuDay>> GetWeekmenuDays()
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync();

        return [.. dbContext.WeekmenuDays.AsNoTracking().OrderBy(p => p.Order)];
    }

    public async Task UpdateWeekmenuDays(IList<WeekmenuDay> weekmenuDays)
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync();
        // WeekmenuDays verwijderen die wel in DB staan maar niet meer in weekmenuDays
        var weekmenuDaysToDelete = await dbContext.WeekmenuDays
            .Where(dbWeekmenuDay => !weekmenuDays.Select(p => p.Id).Contains(dbWeekmenuDay.Id))
            .ToListAsync();
        dbContext.WeekmenuDays.RemoveRange(weekmenuDaysToDelete);

        // WeekmenuDays toevoegen of aanpassen
        for (var i = 0; i < weekmenuDays.Count; i++)
        {
            var weekmenuDay = weekmenuDays[i];
            if (weekmenuDay.Id == 0)
            {
                dbContext.WeekmenuDays.Add(weekmenuDay);
            }
            else
            {
                var dbWeekmenuDay = dbContext.WeekmenuDays.Single(p => p.Id == weekmenuDay.Id);
                dbWeekmenuDay.Update(weekmenuDay.Weekday, weekmenuDay.Name, weekmenuDay.Order);
            }
        }

        await dbContext.SaveChangesAsync();
    }
}
