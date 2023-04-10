using Business.Entities;
using Business.Interfaces;
using Data.Repository.Interfaces;

namespace Business.Components;

public class GetHighlightsQuery : IGetHighlightsQuery
{
    private readonly IPhotographyRepository _photographyRepository;

    public GetHighlightsQuery(IPhotographyRepository photographyRepository) => 
        _photographyRepository = photographyRepository;

    public async Task<IReadOnlyCollection<Highlight>> Execute()
    {
        var sections = (await _photographyRepository.GetSections()).OrderBy(section => section.StartDistance).ToList();
        var places = (await _photographyRepository.GetPlaces()).Select(place => place.Map());
        var hikerUpdates = (await _photographyRepository.GetHikerUpdates()).Select(hikerUpdate => hikerUpdate.Map());
        var placeHighlights = places.Concat(hikerUpdates);

        var highlightsNotInSection = placeHighlights;
        var sectionsWithChildren = sections.Select(section =>
        {
            var highlightsInSection = highlightsNotInSection
                .Where(highlight => IsPlaceInSection(highlight, section))
                .OrderBy(highlight => highlight.Distance)
                .ToList();
            highlightsNotInSection = highlightsNotInSection.Where(highlight => !IsPlaceInSection(highlight, section));

            return section.Map(highlightsInSection);
        }).ToList();

        return sectionsWithChildren
            .Concat(highlightsNotInSection.Select(highlight => highlight.Map()))
            .OrderBy(highlight => highlight.Distance)
            .ToList();
    }

    private static bool IsPlaceInSection(PlaceHighlight place, Section section)
    {
        return section.StartDistance <= place.Distance && section.EndDistance > place.Distance;
    }
}
