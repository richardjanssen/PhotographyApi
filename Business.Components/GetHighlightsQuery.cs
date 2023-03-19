using Business.Entities;
using Business.Interfaces;
using Data.Repository.Interfaces;
using Common.Common.Enums;

namespace Business.Components;

public class GetHighlightsQuery : IGetHighlightsQuery
{
    private readonly IPhotographyRepository _photographyRepository;

    public GetHighlightsQuery(IPhotographyRepository photographyRepository) => 
        _photographyRepository = photographyRepository;

    public async Task<IReadOnlyCollection<Highlight>> Execute()
    {
        var hikerHighlights = (await _photographyRepository.GetHikerUpdates()).Select(hikerUpdate => hikerUpdate.Map(HighlightType.Place));
        return hikerHighlights.ToList();
    }
}
