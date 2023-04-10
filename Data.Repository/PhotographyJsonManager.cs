using Data.Repository.Entities;
using Data.Repository.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace Data.Repository;

public class PhotographyJsonManager : IPhotographyManager
{
    private readonly string _dataBasePath = "data/";
    private readonly string _albumBasePath;
    private readonly string _photosPath;
    private readonly string _accountsPath;
    private readonly string _albumsPath;
    private readonly string _sectionsPath;
    private readonly string _placesPath;
    private readonly string _hikerUpdatesPath;
    private readonly string _hikerLocationsPath;

    public PhotographyJsonManager(IWebHostEnvironment environment)
    {
        _albumBasePath = Path.Combine(environment.ContentRootPath, $"{_dataBasePath}/albums/");
        _photosPath = Path.Combine(environment.ContentRootPath, $"{_dataBasePath}photos.json");
        _accountsPath = Path.Combine(environment.ContentRootPath, $"{_dataBasePath}accounts.json");
        _albumsPath = Path.Combine(environment.ContentRootPath, $"{_dataBasePath}albums.json");
        _sectionsPath = Path.Combine(environment.ContentRootPath, $"{_dataBasePath}sections.json");
        _placesPath = Path.Combine(environment.ContentRootPath, $"{_dataBasePath}places.json");
        _hikerUpdatesPath = Path.Combine(environment.ContentRootPath, $"{_dataBasePath}hiker_updates.json");
        _hikerLocationsPath = Path.Combine(environment.ContentRootPath, $"{_dataBasePath}hiker_locations.json");
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

    public async Task<AlbumDetails> GetAlbumDetails(string fileName)
    {
        var albumPath = Path.Combine(_albumBasePath, fileName);

        if (!File.Exists(albumPath)) return CreateEmptyAlbumDetails();

        var jsonData = await File.ReadAllTextAsync(albumPath);

        if (string.IsNullOrWhiteSpace(jsonData)) return CreateEmptyAlbumDetails();

        return JsonConvert.DeserializeObject<AlbumDetails>(jsonData) ?? CreateEmptyAlbumDetails();
    }

    public async Task WriteAlbumDetails(string fileName, AlbumDetails albumDetails)
    {
        Directory.CreateDirectory(_albumBasePath);
        var albumPath = Path.Combine(_albumBasePath, fileName);
        var jsonData = JsonConvert.SerializeObject(albumDetails);
        await File.WriteAllTextAsync(albumPath, jsonData);
    }

    public async Task<IReadOnlyCollection<Section>> GetSections()
    {
        if (!File.Exists(_sectionsPath)) return new List<Section>();

        var jsonData = await File.ReadAllTextAsync(_sectionsPath);

        if (string.IsNullOrWhiteSpace(jsonData)) return new List<Section>();

        return JsonConvert.DeserializeObject<List<Section>>(jsonData) ?? new List<Section>();
    }

    public async Task<IReadOnlyCollection<Place>> GetPlaces()
    {
        if (!File.Exists(_placesPath)) return new List<Place>();

        var jsonData = await File.ReadAllTextAsync(_placesPath);

        if (string.IsNullOrWhiteSpace(jsonData)) return new List<Place>();

        return JsonConvert.DeserializeObject<List<Place>>(jsonData) ?? new List<Place>();
    }

    public async Task<IReadOnlyCollection<HikerUpdate>> GetHikerUpdates()
    {
        if (!File.Exists(_hikerUpdatesPath)) return new List<HikerUpdate>();

        var jsonData = await File.ReadAllTextAsync(_hikerUpdatesPath);

        if (string.IsNullOrWhiteSpace(jsonData)) return new List<HikerUpdate>();

        return JsonConvert.DeserializeObject<List<HikerUpdate>>(jsonData) ?? new List<HikerUpdate>();
    }

    public async Task<IReadOnlyCollection<HikerLocation>> GetHikerLocations()
    {
        if (!File.Exists(_hikerLocationsPath)) return new List<HikerLocation>();

        var jsonData = await File.ReadAllTextAsync(_hikerLocationsPath);

        if (string.IsNullOrWhiteSpace(jsonData)) return new List<HikerLocation>();

        return JsonConvert.DeserializeObject<List<HikerLocation>>(jsonData) ?? new List<HikerLocation>();
    }

    public async Task WriteHikerUpdates(IReadOnlyCollection<HikerUpdate> hikerUpdates)
    {
        var jsonData = JsonConvert.SerializeObject(hikerUpdates);
        await File.WriteAllTextAsync(_hikerUpdatesPath, jsonData);
    }

    private static AlbumDetails CreateEmptyAlbumDetails() => new()
    {
        Photos = new List<Photo>()
    };
}
