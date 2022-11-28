using Data.Repository.Interfaces;

namespace Data.Repository;

public class PhotographyJsonRepository : IPhotographyRepository
{
    private readonly IPhotographyManager _photographyManager;

    public PhotographyJsonRepository(IPhotographyManager photographyManager) => _photographyManager = photographyManager;

    public async Task<IEnumerable<Business.Entities.Photo>> GetPhotos() =>
        (await _photographyManager.GetPhotos()).Select(photo => photo.Map()).ToList();

    public async Task<Business.Entities.Photo> AddPhoto(Business.Entities.Photo photo)
    {
        var currentPhotos = (await _photographyManager.GetPhotos()).ToList();

        var id = currentPhotos.Count > 0 ? currentPhotos.Select(photo => photo.Id).Max() + 1 : 1;
        currentPhotos.Add(photo.Map(id));

        await _photographyManager.WritePhotos(currentPhotos);

        return photo;
    }

    public async Task<Business.Entities.Account?> GetAccountByUserName(string userName) =>
        (await _photographyManager.GetAccounts()).SingleOrDefault(account => account.UserName == userName)?.Map();
}
