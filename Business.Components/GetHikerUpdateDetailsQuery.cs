using Business.Entities;
using Business.Interfaces;
using Data.Interfaces;

namespace Business.Components;

public class GetHikerUpdateDetailsQuery : IGetHikerUpdateDetailsQuery
{
    private readonly IPhotographyRepository _photographyRepository;

    public GetHikerUpdateDetailsQuery(IPhotographyRepository photographyRepository) => 
        _photographyRepository = photographyRepository;

    public async Task<HikerUpdateDetails> Execute(int id)
    {
        var hikerUpdate = await _photographyRepository.GetHikerUpdateById(id);
        if (hikerUpdate == null)
        {
            return new(null, null);
        }

        var album = hikerUpdate.AlbumId != null ? await _photographyRepository.GetAlbumById((int)hikerUpdate.AlbumId) : null;
        return new(hikerUpdate.Text, album);
    }
}
