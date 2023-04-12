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
        var hikerLocation = (await _photographyRepository.GetHikerLocations()).OrderByDescending(location => location.Date).FirstOrDefault();
        var places = (await _photographyRepository.GetPlaces()).Select(place => place.Map());
        var hikerLocationHighlightList = hikerLocation == null
            ? new List<PointWithDistance>()
            : new List<PointWithDistance>() { new(null, "Current location", Common.Common.Enums.PlaceHighlightType.Location, hikerLocation!.Distance) };
        var hikerUpdates = (await _photographyRepository.GetHikerUpdates()).Select(hikerUpdate => hikerUpdate.Map());
        var placeHighlights = places.Concat(hikerUpdates).Concat(hikerLocationHighlightList);

        var placeHighlightsNotInSection = placeHighlights;
        var sectionsWithChildren = sections.Select(section =>
        {
            var highlightsInSection = placeHighlightsNotInSection
                .Where(highlight => IsPointInSection(highlight, section))
                .OrderBy(highlight => highlight.Distance)
                .ToList();
            placeHighlightsNotInSection = placeHighlightsNotInSection.Where(highlight => !IsPointInSection(highlight, section));

            return section.Map(highlightsInSection);
        }).ToList();

        var highlightsNotInSection = placeHighlightsNotInSection.ToList().Map().Select(pointHighlight => pointHighlight.Map());
        return sectionsWithChildren
            .Concat(highlightsNotInSection)
            .OrderBy(highlight =>
                highlight.Type == Common.Common.Enums.HighlightType.Section
                    ? highlight.SectionHighlight!.StartDistance
                    : highlight.PointHighlight!.Distance)
            .ToList();
    }

    private static bool IsPointInSection(PointWithDistance point, Section section)
    {
        return section.StartDistance <= point.Distance && section.EndDistance > point.Distance;
    }
}
