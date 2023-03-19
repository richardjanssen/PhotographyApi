using Data.Repository.Entities;

namespace Data.Repository.Interfaces;

public interface IPhotographyManager
{
    Task<IReadOnlyCollection<Photo>> GetPhotos();
    Task WritePhotos(IReadOnlyCollection<Photo> photos);
    Task<IReadOnlyCollection<Account>> GetAccounts();
    Task<IReadOnlyCollection<Album>> GetAlbums();
    Task WriteAlbums(IReadOnlyCollection<Album> photos);
    Task<AlbumDetails> GetAlbumDetails(string fileName);
    Task WriteAlbumDetails(string fileName, AlbumDetails albumDetails);
    Task<IReadOnlyCollection<HikerUpdate>> GetHikerUpdates();
    Task WriteHikerUpdates(IReadOnlyCollection<HikerUpdate> hikerUpdates);
}
