using Business.Entities.Dto;

namespace Data.Interfaces;

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
    Task<IEnumerable<Place>> GetPlaces();
    Task<IEnumerable<HikerUpdate>> GetHikerUpdates();
    Task<HikerUpdate?> GetHikerUpdateById(int id);
    Task<HikerUpdate> AddHikerUpdate(HikerUpdate addHikerUpdate);
    Task<IEnumerable<HikerLocation>> GetHikerLocations();
    Task<HikerLocation> AddHikerLocation(HikerLocation hikerLocation);
}
