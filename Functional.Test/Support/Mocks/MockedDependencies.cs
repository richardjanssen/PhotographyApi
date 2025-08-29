using Data.Repository.Interfaces;
using Moq;
using System.Reflection;

namespace Functional.Test.Support.Mocks;
public class MockedDependencies
{
    public MockedDependencies()
    {
        DbContextManager = new FakeDbContextManager();
    }

    private readonly IReadOnlyCollection<string> _fakes = new List<string>() { nameof(DbContextManager) };
    public FakeDbContextManager DbContextManager { get; }

    public Mock<IPhotographyManager> PhotographyManager { get; } = new Mock<IPhotographyManager>();

    public IEnumerable<(Type, object)> GetMockedDependencies()
    {
        return GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(property => !_fakes.Contains(property.Name))
            .Select(property =>
        {
            var underlyingType = property.PropertyType.GetGenericArguments()[0];
            var value = property.GetValue(this) as Mock;

            return (underlyingType, value!.Object);
        });
    }
}
