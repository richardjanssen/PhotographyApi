using Data.Repository.Entities;
using Data.Repository.Interfaces;

namespace Data.Repository;

public class FakePhotographyManager : IPhotographyManager
{
    private IReadOnlyCollection<Album> _albums = new List<Album>();
    private readonly IReadOnlyCollection<Account> _accounts = new List<Account>();
    private readonly IDictionary<string, AlbumDetails> _albumDetails = new Dictionary<string, AlbumDetails>();

    public async Task<IReadOnlyCollection<Account>> GetAccounts() => await Task.Run(() => _accounts);

    public async Task<IReadOnlyCollection<Album>> GetAlbums() => await Task.Run(() => _albums);

    public async Task WriteAlbums(IReadOnlyCollection<Album> albums) => await Task.Run(() => _albums = albums);

    public async Task<AlbumDetails> GetAlbumDetails(string fileName) => await Task.Run(() => _albumDetails[fileName]);

    public async Task WriteAlbumDetails(string fileName, AlbumDetails albumDetails) => await Task.Run(() => _albumDetails[fileName] = albumDetails);

    public Task<IReadOnlyCollection<HikerUpdate>> GetHikerUpdates()
    {
        throw new NotImplementedException();
    }

    public Task WriteHikerUpdates(IReadOnlyCollection<HikerUpdate> hikerUpdates)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Section>> GetSections()
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Place>> GetPlaces()
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<HikerLocation>> GetHikerLocations()
    {
        throw new NotImplementedException();
    }

    public Task WriteHikerLocations(IReadOnlyCollection<HikerLocation> hikerLocations)
    {
        throw new NotImplementedException();
    }

    public Task<Settings> GetSettings()
    {
        throw new NotImplementedException();
    }

    public Task WriteSettings(Settings settings)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<DistanceMarker>> GetTrail()
    {
        throw new NotImplementedException();
    }
}
