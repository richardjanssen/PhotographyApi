using Business.Entities.Weekmenu;

namespace Data.Interfaces;

public interface IWeekmenuDayRepository
{
    Task<IReadOnlyCollection<WeekmenuDay>> GetWeekmenuDays();
    Task UpdateWeekmenuDays(IList<WeekmenuDay> weekmenuDays);
}