using Business.Entities.Dto;
using Business.Interfaces.HikerUpdates;
using Data.Interfaces;

namespace Business.Components.HikerUpdates;

public class GetHikerUpdatesQuery : IGetHikerUpdatesQuery
{
    private readonly IPhotographyRepository _photographyRepository;

    public GetHikerUpdatesQuery(IPhotographyRepository photographyRepository) =>
        _photographyRepository = photographyRepository;

    public async Task<IReadOnlyCollection<HikerUpdate>> Execute() =>
        (await _photographyRepository.GetHikerUpdates())
        .OrderByDescending(update => update.Date)
        .ToList();
}
