using Business.Interfaces.HikerUpdates;
using Data.Interfaces;

namespace Business.Components.HikerUpdates;

public class DeleteHikerUpdateQuery : IDeleteHikerUpdateQuery
{
    private readonly IPhotographyRepository _photographyRepository;

    public DeleteHikerUpdateQuery(IPhotographyRepository photographyRepository) => _photographyRepository = photographyRepository;

    public async Task Execute(int id) => await _photographyRepository.DeleteHikerUpdate(id);
}
