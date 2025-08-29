using Data.Repository.Database;
using Microsoft.EntityFrameworkCore;

namespace Functional.Test.Support.Mocks;

public class FakeDbContextManager
{
    public static RiesjDbContext GetDbContext()
    {
        return new RiesjDbContext(GetOptionsBuilder().Options, new FakeDateTimeProvider());
    }

    public static DbContextOptionsBuilder<RiesjDbContext> GetOptionsBuilder()
    {
        return new DbContextOptionsBuilder<RiesjDbContext>().UseSqlite("Data Source=Data/Riesj_AT.db");
    }
}