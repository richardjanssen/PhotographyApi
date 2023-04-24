using Business.Entities.Dto;
using Business.Entities.Highlights;
using Business.Interfaces;
using Common.Common.Enums;
using Data.Interfaces;

namespace Business.Components.GetHighlights;

public class GetHighlightsQuery : IGetHighlightsQuery
{
    private readonly IPhotographyRepository _photographyRepository;

    public GetHighlightsQuery(IPhotographyRepository photographyRepository) =>
        _photographyRepository = photographyRepository;

    public async Task<IReadOnlyCollection<Highlight>> Execute()
    {
        var sectionsTask = _photographyRepository.GetSections();
        var pointsTask = GetPoints();

        var pointsNotInSection = await GetPoints();
        var sections = (await sectionsTask).OrderBy(section => section.StartDistance).ToList();
        var sectionsWithChildren = sections.Select(section =>
        {
            var highlightsInSection = pointsNotInSection
                .Where(highlight => IsPointInSection(highlight, section))
                .OrderBy(highlight => highlight.Distance)
                .ToList();
            pointsNotInSection = pointsNotInSection.Where(highlight => !IsPointInSection(highlight, section));

            return section.Map(highlightsInSection);
        }).ToList();

        var highlightsNotInSection = pointsNotInSection.ToList().Map().Select(pointHighlight => pointHighlight.Map());

        return sectionsWithChildren
            .Concat(highlightsNotInSection)
            .OrderBy(highlight =>
                highlight.Type == HighlightType.section
                    ? highlight.SectionHighlight!.StartDistance
                    : highlight.PointHighlight!.Distance)
            .ToList();
    }

    private async Task<IEnumerable<PointWithDistance>> GetPoints()
    {
        var placesTask = _photographyRepository.GetPlaces();
        var hikerUpdatesTask = _photographyRepository.GetHikerUpdates();
        var hikerLocationsTask = _photographyRepository.GetHikerLocations();

        var places = (await placesTask).Select(place => place.Map());
        var hikerUpdates = (await hikerUpdatesTask).Select(hikerUpdate => hikerUpdate.Map());
        var hikerLocation = (await hikerLocationsTask).OrderByDescending(location => location.Date).FirstOrDefault();
        var hikerLocationHighlightList = hikerLocation == null
            ? new List<PointWithDistance>()
            : new List<PointWithDistance>() { new(null, "Current location", PlaceHighlightType.location, hikerLocation!.Distance) };

        return places.Concat(hikerUpdates).Concat(hikerLocationHighlightList);
    }
    private static bool IsPointInSection(PointWithDistance point, Section section)
    {
        return section.StartDistance <= point.Distance && section.EndDistance > point.Distance;
    }
}
