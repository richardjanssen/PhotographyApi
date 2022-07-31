using System.Data.Common;
using Data.Repository;
using Data.Repository.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Test.Helpers;

public sealed class FakePhotographyDbContext
{
    private DbConnection _connection = null!;

    public FakePhotographyDbContext() => InitializeConnection();

    public PhotographyDbContext GetContext()
    {
        return new PhotographyDbContext(CreateOptions());
    }

    public void Seed(IReadOnlyCollection<Photo> photos)
    {
        using var context = GetContext();
        context.AddRange(photos);
        context.SaveChanges();
    }

    private void InitializeConnection()
    {
        if (_connection != null)
        {
            _connection.Close();
        }

        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        using var context = GetContext();
        context.Database.EnsureCreated();
    }

    private DbContextOptions<PhotographyDbContext> CreateOptions() =>
        new DbContextOptionsBuilder<PhotographyDbContext>().UseSqlite(_connection).Options;
}
