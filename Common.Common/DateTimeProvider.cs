using Common.Common.Interfaces;

namespace Common.Common;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow() => DateTime.UtcNow;
}
