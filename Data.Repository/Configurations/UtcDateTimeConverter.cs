using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Repository.Configurations;

public class UtcDateTimeConverter : ValueConverter<DateTime, DateTime>
{
    public UtcDateTimeConverter() : base(v => v.ToUniversalTime(), v => new DateTime(v.Ticks, DateTimeKind.Utc))
    {
    }
}
