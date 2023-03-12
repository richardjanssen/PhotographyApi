using Data.Repository.Entities;
using Data.Repository.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace Data.Repository;

public class PhotographyJsonManager : IPhotographyManager
{
    private readonly string _photosPath;
    private readonly string _accountsPath;
    private readonly string _albumsPath;

    public PhotographyJsonManager(IWebHostEnvironment environment)
    {
        const string dataBasePath = "data/";
        _photosPath = Path.Combine(environment.ContentRootPath, $"{dataBasePath}photos.json");
        _accountsPath = Path.Combine(environment.ContentRootPath, $"{dataBasePath}accounts.json");
        _albumsPath = Path.Combine(environment.ContentRootPath, $"{dataBasePath}albums.json");
    }

    public async Task<IReadOnlyCollection<Photo>> GetPhotos()
    {
        var jsonData = await File.ReadAllTextAsync(_photosPath);

        if (string.IsNullOrWhiteSpace(jsonData)) return new List<Photo>();

        return JsonConvert.DeserializeObject<List<Photo>>(jsonData) ?? new List<Photo>();
    }

    public async Task WritePhotos(IReadOnlyCollection<Photo> photos)
    {
        var jsonData = JsonConvert.SerializeObject(photos);
        await File.WriteAllTextAsync(_photosPath, jsonData);
    }

    public async Task<IReadOnlyCollection<Account>> GetAccounts()
    {
        var jsonData = await File.ReadAllTextAsync(_accountsPath);

        if (string.IsNullOrWhiteSpace(jsonData)) throw new Exception();

        return JsonConvert.DeserializeObject<List<Account>>(jsonData) ?? new List<Account>();
    }

    public async Task<IReadOnlyCollection<Album>> GetAlbums()
    {
        if (!File.Exists(_albumsPath)) return new List<Album>();

        var jsonData = await File.ReadAllTextAsync(_albumsPath);

        if (string.IsNullOrWhiteSpace(jsonData)) return new List<Album>();

        return JsonConvert.DeserializeObject<List<Album>>(jsonData) ?? new List<Album>();
    }

    public async Task WriteAlbums(IReadOnlyCollection<Album> albums)
    {
        var jsonData = JsonConvert.SerializeObject(albums);
        await File.WriteAllTextAsync(_albumsPath, jsonData);
    }
}
