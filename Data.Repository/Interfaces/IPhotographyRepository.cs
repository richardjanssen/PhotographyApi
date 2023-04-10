using Business.Entities;

namespace Data.Repository.Interfaces;

public interface IPhotographyRepository
{
    Task<IEnumerable<Photo>> GetPhotos();
    Task<Photo> AddPhoto(Photo photo);
    Task<Photo> AddAlbumPhoto(Photo photo, int albumId);
    Task<Account?> GetAccountByUserName(string userName);
    Task<IEnumerable<Album>> GetAlbums();
    Task<AlbumDetails> GetAlbumById(int id);
    Task<Album> AddAlbum(Album album);
    Task<IEnumerable<Section>> GetSections();
    Task<IEnumerable<AddHikerUpdate>> GetHikerUpdates();
    Task<AddHikerUpdate> AddHikerUpdate(AddHikerUpdate addHikerUpdate);
}
