using Common.Common.Interfaces;

namespace Common.Common;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime Now => DateTime.Now;

    public DateTime Today => DateTime.Today;
}
