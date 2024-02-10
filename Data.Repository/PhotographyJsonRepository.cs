using Business.Entities.Dto;
using Data.Interfaces;
using Data.Repository.Interfaces;

namespace Data.Repository;

public class PhotographyJsonRepository(IPhotographyManager photographyManager) : IPhotographyRepository
{
    public async Task<IEnumerable<Photo>> GetPhotos() =>
        (await photographyManager.GetPhotos()).Select(photo => photo.Map()).ToList();

    public async Task<Photo> AddPhoto(Photo photo)
    {
        if (photo.Id != 0)
        {
            throw new ArgumentException("photo.Id should be 0 when creating a new photo");
        }

        var currentPhotos = (await photographyManager.GetPhotos()).ToList();

        var id = currentPhotos.Count > 0 ? currentPhotos.Select(photo => photo.Id).Max() + 1 : 1;
        currentPhotos.Add(photo.Map(id));

        await photographyManager.WritePhotos(currentPhotos);

        return photo;
    }

    public async Task<Photo> AddAlbumPhoto(Photo photo, int albumId)
    {
        if (photo.Id != 0)
        {
            throw new ArgumentException("photo.Id should be 0 when creating a new photo");
        }

        var album = (await photographyManager.GetAlbums()).ToList().FirstOrDefault(album => album.Id == albumId)
            ?? throw new InvalidOperationException($"Cannot add photo to album with id {albumId}. Album does not exist");

        var albumDetails = await photographyManager.GetAlbumDetails(album.FileName);
        var albumPhotos = albumDetails.Photos.ToList();

        var id = albumPhotos.Count > 0 ? albumPhotos.Select(photo => photo.Id).Max() + 1 : 1;
        albumPhotos.Add(photo.Map(id));
        albumDetails.Photos = albumPhotos;

        await photographyManager.WriteAlbumDetails(album.FileName, albumDetails);

        return photo;
    }

    public async Task DeleteAlbumPhoto(int albumId, int photoId)
    {
        var album = (await photographyManager.GetAlbums()).ToList().FirstOrDefault(album => album.Id == albumId)
            ?? throw new InvalidOperationException($"Cannot delete photo from album with id {albumId}. Album does not exist");

        var albumDetails = await photographyManager.GetAlbumDetails(album.FileName);
        albumDetails.Photos = albumDetails.Photos.Where(photo => photo.Id != photoId).ToList();

        await photographyManager.WriteAlbumDetails(album.FileName, albumDetails);
    }

    public async Task<IEnumerable<Album>> GetAlbums() =>
        (await photographyManager.GetAlbums()).Select(album => album.Map()).ToList();

    public async Task<AlbumDetails> GetAlbumById(int id)
    {
        var album = (await photographyManager.GetAlbums()).First(album => album.Id == id);
        return (await photographyManager.GetAlbumDetails(album.FileName)).Map();
    }

    public async Task<Album> AddAlbum(Album album)
    {
        if (album.Id != 0)
        {
            throw new ArgumentException("album.Id should be 0 when creating a new album");
        }

        var currentAlbums = (await photographyManager.GetAlbums()).ToList();

        var id = currentAlbums.Count > 0 ? currentAlbums.Select(album => album.Id).Max() + 1 : 1;
        var fileName = $"album_{id}.json";
        currentAlbums.Add(album.Map(id, fileName));

        await photographyManager.WriteAlbums(currentAlbums);

        return album;
    }

    public async Task<IEnumerable<Section>> GetSections() =>
        (await photographyManager.GetSections()).Select(section => section.Map()).ToList();

    public async Task<IEnumerable<HikerUpdate>> GetHikerUpdates() =>
        (await photographyManager.GetHikerUpdates()).Select(hikerUpdate => hikerUpdate.Map()).ToList();

    public async Task<HikerUpdate?> GetHikerUpdateById(int id) =>
        (await photographyManager.GetHikerUpdates()).FirstOrDefault(update => update.Id == id)?.Map();

    public async Task<HikerUpdate> AddHikerUpdate(HikerUpdate addHikerUpdate)
    {
        if (addHikerUpdate.Id != 0)
        {
            throw new ArgumentException("addHikerUpdate.Id should be 0 when creating a new hiker update");
        }

        var currentHikerUpdates = (await photographyManager.GetHikerUpdates()).ToList();

        var id = currentHikerUpdates.Count > 0 ? currentHikerUpdates.Select(update => update.Id).Max() + 1 : 1;
        currentHikerUpdates.Add(addHikerUpdate.Map(id));

        await photographyManager.WriteHikerUpdates(currentHikerUpdates);

        return addHikerUpdate;
    }

    public async Task<HikerUpdate> UpdateHikerUpdate(HikerUpdate hikerUpdate)
    {
        var currentHikerUpdates = (await photographyManager.GetHikerUpdates()).ToList();
        foreach (var update in currentHikerUpdates.Where(currentUpdate => currentUpdate.Id == hikerUpdate.Id))
        {
            // Id and date are never updated, because these are not user provided
            update.Title = hikerUpdate.Title;
            update.Type = hikerUpdate.Type;
            update.Distance = hikerUpdate.Distance;
            update.AlbumId = hikerUpdate.AlbumId;
            update.PlaceId = hikerUpdate.PlaceId;
            update.Text = hikerUpdate.Text;
        }

        await photographyManager.WriteHikerUpdates(currentHikerUpdates);

        return hikerUpdate;
    }

    public async Task DeleteHikerUpdate(int id)
    {
        var currentHikerUpdates = (await photographyManager.GetHikerUpdates()).ToList();
        var newHikerUpdates = currentHikerUpdates.Where(location => location.Id != id).ToList();
        await photographyManager.WriteHikerUpdates(newHikerUpdates);
    }

    public async Task<HikerLocation> AddHikerLocation(HikerLocation hikerLocation)
    {
        if (hikerLocation.Id != 0)
        {
            throw new ArgumentException("hikerLocation.Id should be 0 when creating a new hiker update");
        }

        var currentHikerLocations = (await photographyManager.GetHikerLocations()).ToList();

        var id = currentHikerLocations.Count > 0 ? currentHikerLocations.Select(location => location.Id).Max() + 1 : 1;
        currentHikerLocations.Add(hikerLocation.Map(id));

        await photographyManager.WriteHikerLocations(currentHikerLocations);

        return hikerLocation;
    }

    public async Task<IEnumerable<HikerLocation>> GetHikerLocations() =>
    (await photographyManager.GetHikerLocations()).Select(hikerLocation => hikerLocation.Map()).ToList();

    public async Task<HikerLocation?> GetHikerLocationById(int id) =>
        (await photographyManager.GetHikerLocations()).FirstOrDefault(location => location.Id == id)?.Map();

    public async Task DeleteLocation(int id)
    {
        var currentHikerLocations = (await photographyManager.GetHikerLocations()).ToList();
        var newHikerLocations = currentHikerLocations.Where(location => location.Id != id).ToList();
        await photographyManager.WriteHikerLocations(newHikerLocations);
    }

    public async Task<Account?> GetAccountByUserName(string userName) =>
        (await photographyManager.GetAccounts()).SingleOrDefault(account => account.UserName == userName)?.Map();
}
