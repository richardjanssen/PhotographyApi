using Business.Entities.Dto;
using Data.Interfaces;
using Data.Repository.Interfaces;

namespace Data.Repository;

public class PhotographyJsonRepository : IPhotographyRepository
{
    private readonly IPhotographyManager _photographyManager;

    public PhotographyJsonRepository(IPhotographyManager photographyManager) => _photographyManager = photographyManager;

    public async Task<IEnumerable<Photo>> GetPhotos() =>
        (await _photographyManager.GetPhotos()).Select(photo => photo.Map()).ToList();

    public async Task<Photo> AddPhoto(Photo photo)
    {
        if (photo.Id != 0)
        {
            throw new ArgumentException("photo.Id should be 0 when creating a new photo");
        }

        var currentPhotos = (await _photographyManager.GetPhotos()).ToList();

        var id = currentPhotos.Count > 0 ? currentPhotos.Select(photo => photo.Id).Max() + 1 : 1;
        currentPhotos.Add(photo.Map(id));

        await _photographyManager.WritePhotos(currentPhotos);

        return photo;
    }

    public async Task<Photo> AddAlbumPhoto(Photo photo, int albumId)
    {
        if (photo.Id != 0)
        {
            throw new ArgumentException("photo.Id should be 0 when creating a new photo");
        }

        var album = (await _photographyManager.GetAlbums()).ToList().FirstOrDefault(album => album.Id == albumId)
            ?? throw new InvalidOperationException($"Cannot add photo to album with id {albumId}. Album does not exist");

        var albumDetails = await _photographyManager.GetAlbumDetails(album.FileName);
        var albumPhotos = albumDetails.Photos.ToList();

        var id = albumPhotos.Count > 0 ? albumPhotos.Select(photo => photo.Id).Max() + 1 : 1;
        albumPhotos.Add(photo.Map(id));
        albumDetails.Photos = albumPhotos;

        await _photographyManager.WriteAlbumDetails(album.FileName, albumDetails);

        return photo;
    }

    public async Task<IEnumerable<Album>> GetAlbums() =>
        (await _photographyManager.GetAlbums()).Select(album => album.Map()).ToList();

    public async Task<AlbumDetails> GetAlbumById(int id)
    {
        var album = (await _photographyManager.GetAlbums()).First(album => album.Id == id);
        return (await _photographyManager.GetAlbumDetails(album.FileName)).Map();
    }

    public async Task<Album> AddAlbum(Album album)
    {
        if (album.Id != 0)
        {
            throw new ArgumentException("album.Id should be 0 when creating a new album");
        }

        var currentAlbums = (await _photographyManager.GetAlbums()).ToList();

        var id = currentAlbums.Count > 0 ? currentAlbums.Select(album => album.Id).Max() + 1 : 1;
        var fileName = $"album_{id}.json";
        currentAlbums.Add(album.Map(id, fileName));

        await _photographyManager.WriteAlbums(currentAlbums);

        return album;
    }

    public async Task<IEnumerable<Section>> GetSections() =>
        (await _photographyManager.GetSections()).Select(section => section.Map()).ToList();

    public async Task<IEnumerable<Place>> GetPlaces() =>
        (await _photographyManager.GetPlaces()).Select(place => place.Map()).ToList();

    public async Task<IEnumerable<HikerUpdate>> GetHikerUpdates() =>
        (await _photographyManager.GetHikerUpdates()).Select(hikerUpdate => hikerUpdate.Map()).ToList();

    public async Task<HikerUpdate?> GetHikerUpdateById(int id) =>
        (await _photographyManager.GetHikerUpdates()).FirstOrDefault(update => update.Id == id)?.Map();

    public async Task<HikerUpdate> AddHikerUpdate(HikerUpdate addHikerUpdate)
    {
        if (addHikerUpdate.Id != 0)
        {
            throw new ArgumentException("addHikerUpdate.Id should be 0 when creating a new hiker update");
        }

        var currentHikerUpdates = (await _photographyManager.GetHikerUpdates()).ToList();

        var id = currentHikerUpdates.Count > 0 ? currentHikerUpdates.Select(album => album.Id).Max() + 1 : 1;
        currentHikerUpdates.Add(addHikerUpdate.Map(id));

        await _photographyManager.WriteHikerUpdates(currentHikerUpdates);

        return addHikerUpdate;
    }

    public async Task<HikerLocation> AddHikerLocation(HikerLocation hikerLocation)
    {
        if (hikerLocation.Id != 0)
        {
            throw new ArgumentException("hikerLocation.Id should be 0 when creating a new hiker update");
        }

        var currentHikerLocations = (await _photographyManager.GetHikerLocations()).ToList();

        var id = currentHikerLocations.Count > 0 ? currentHikerLocations.Select(location => location.Id).Max() + 1 : 1;
        currentHikerLocations.Add(hikerLocation.Map(id));

        await _photographyManager.WriteHikerLocations(currentHikerLocations);

        return hikerLocation;
    }

    public async Task<IEnumerable<HikerLocation>> GetHikerLocations() =>
    (await _photographyManager.GetHikerLocations()).Select(hikerLocation => hikerLocation.Map()).ToList();

    public async Task<Account?> GetAccountByUserName(string userName) =>
        (await _photographyManager.GetAccounts()).SingleOrDefault(account => account.UserName == userName)?.Map();
}
