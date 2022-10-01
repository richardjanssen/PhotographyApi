using Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class PhotographyRepository : IPhotographyRepository
{
    public PhotographyRepository(PhotographyDbContext photographyContext) => _photographyContext = photographyContext;

    private readonly PhotographyDbContext _photographyContext;

    public async Task<IEnumerable<Business.Entities.Photo>> GetPhotos() => await _photographyContext.Photos
        .Include(photo => photo.Images)
        .Select(photo => photo.Map())
        .ToListAsync();

    public Business.Entities.Photo AddPhoto(Business.Entities.Photo photo)
    {
        var result = _photographyContext.Add(photo.Map()).Entity;
        _photographyContext.SaveChanges();

        return result.Map();
    }

    public async Task<Business.Entities.Account?> GetAccountByUserName(string userName)
    {
        var account = await _photographyContext.Accounts
            .Where(account => account.UserName == userName)
            .SingleOrDefaultAsync();

        return account?.Map();
    }

    public async Task AddAccount(Business.Entities.Account account)
    {
        await _photographyContext.AddAsync(account.Map());
        _photographyContext.SaveChanges();
    }
}
