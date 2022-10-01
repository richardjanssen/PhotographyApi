using Business.Entities;

namespace Data.Repository.Interfaces;

public interface IPhotographyRepository
{
    Task<IEnumerable<Photo>> GetPhotos();
    Photo AddPhoto(Photo photo);
    Task<Account?> GetAccountByUserName(string userName);
    Task AddAccount(Account account);

}
