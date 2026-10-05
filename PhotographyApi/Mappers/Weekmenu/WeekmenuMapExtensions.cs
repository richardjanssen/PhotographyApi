using Business.Entities.Weekmenu;
using PhotographyApi.ViewModels.Weekmenu;

namespace PhotographyApi.Mappers.Weekmenu;

public static class WeekmenuMapExtensions
{
    public static WeekmenuDayViewModel Map(this WeekmenuDay weekmenuDay) => new(
        weekmenuDay.Id,
        weekmenuDay.RowVersion,
        weekmenuDay.Weekday.Map(),
        weekmenuDay.Name);

    public static WeekmenuDay Map(this WeekmenuDayViewModel weekmenuDay, int order) => new(
        weekmenuDay.Weekday.Map(),
        weekmenuDay.Name,
        order)
    {
        Id = weekmenuDay.Id ?? 0,
        RowVersion = weekmenuDay.RowVersion ?? 0
    };

    private static WeekdayViewModel Map(this Weekday weekday)
    {
        switch (weekday)
        {
            case Weekday.Monday:
                return WeekdayViewModel.Monday;
            case Weekday.Tuesday:
                return WeekdayViewModel.Tuesday;
            case Weekday.Wednesday:
                return WeekdayViewModel.Wednesday;
            case Weekday.Thursday:
                return WeekdayViewModel.Thursday;
            case Weekday.Friday:
                return WeekdayViewModel.Friday;
            case Weekday.Saturday:
                return WeekdayViewModel.Saturday;
            case Weekday.Sunday:
                return WeekdayViewModel.Sunday;
            default:
                throw new ArgumentOutOfRangeException($"{nameof(weekday)} cannot be mapped to {typeof(WeekdayViewModel)}");
        }
    }

    private static Weekday Map(this WeekdayViewModel weekday)
    {
        switch (weekday)
        {
            case WeekdayViewModel.Monday:
                return Weekday.Monday;
            case WeekdayViewModel.Tuesday:
                return Weekday.Tuesday;
            case WeekdayViewModel.Wednesday:
                return Weekday.Wednesday;
            case WeekdayViewModel.Thursday:
                return Weekday.Thursday;
            case WeekdayViewModel.Friday:
                return Weekday.Friday;
            case WeekdayViewModel.Saturday:
                return Weekday.Saturday;
            case WeekdayViewModel.Sunday:
                return Weekday.Sunday;
            default:
                throw new ArgumentOutOfRangeException($"{nameof(weekday)} cannot be mapped to {typeof(Weekday)}");
        }
    }
}
