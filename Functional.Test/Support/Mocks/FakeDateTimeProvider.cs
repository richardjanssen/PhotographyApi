using Common.Common.Interfaces;
using Test.Helpers;

namespace Functional.Test.Support.Mocks;

public class FakeDateTimeProvider : IDateTimeProvider
{
    private DateTime _mockedDateTime;

    public FakeDateTimeProvider() => _mockedDateTime = TestConstants.FakeDateTimeToday;

    public FakeDateTimeProvider(DateTime dateTime) => _mockedDateTime = dateTime;

    public FakeDateTimeProvider(int year, int month, int date) => _mockedDateTime = new DateTime(year, month, date);

    public void SetDateTime(DateTime dateTime) => _mockedDateTime = dateTime;

    public DateTime Now => _mockedDateTime;
    public DateTime Today => _mockedDateTime.Date;
    public DateTime UtcNow => _mockedDateTime.AddHours(-1);
}
