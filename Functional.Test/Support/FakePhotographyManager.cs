using Data.Repository.Entities;
using Data.Repository.Interfaces;

namespace Data.Repository;

public class FakePhotographyManager : IPhotographyManager
{
    private IReadOnlyCollection<Photo> _photos = new List<Photo>();
    private readonly IReadOnlyCollection<Account> _accounts = new List<Account>();

    public async Task<IReadOnlyCollection<Photo>> GetPhotos() => await Task.Run(() => _photos);

    public async Task WritePhotos(IReadOnlyCollection<Photo> photos) => await Task.Run(() => _photos = photos);

    public async Task<IReadOnlyCollection<Account>> GetAccounts() => await Task.Run(() => _accounts);
}
