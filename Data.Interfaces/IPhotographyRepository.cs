using Business.Entities.Dto;

namespace Data.Interfaces;

public interface IPhotographyRepository
{
    Task<Photo> AddAlbumPhoto(Photo photo, int albumId);
    Task DeleteAlbumPhoto(int albumId, int photoId);
    Task<Account?> GetAccountByUserName(string userName);
    Task<IEnumerable<Album>> GetAlbums();
    Task<AlbumDetails> GetAlbumById(int id);
    Task<Album> AddAlbum(Album album);
    Task<IEnumerable<Section>> GetSections();
    Task<IEnumerable<HikerUpdate>> GetHikerUpdates();
    Task<HikerUpdate?> GetHikerUpdateById(int id);
    Task<HikerUpdate> AddHikerUpdate(HikerUpdate addHikerUpdate);
    Task<HikerUpdate> UpdateHikerUpdate(HikerUpdate hikerUpdate);
    Task DeleteHikerUpdate(int id);
    Task<IEnumerable<HikerLocation>> GetHikerLocations();
    //Task<HikerLocation?> GetHikerLocationById(int id);
    Task DeleteLocation(int id);
    Task<HikerLocation> AddHikerLocation(HikerLocation hikerLocation);
}
