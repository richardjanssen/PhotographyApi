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
        var places = (await _photographyRepository.GetPlaces()).Select(place => place.Map(IsHikerAtDistance(hikerLocation, place.Distance)));
        var hikerIsAtAPlace = places.Any(place => IsHikerAtDistance(hikerLocation, place.Distance));
        var hikerLocationHighlightList = hikerIsAtAPlace ? new List<PlaceHighlight>() : new List<PlaceHighlight>() { new(null, "Current location", hikerLocation!.Distance, Common.Common.Enums.PlaceHighlightType.Location, true) };
        var hikerUpdates = (await _photographyRepository.GetHikerUpdates()).Select(hikerUpdate => hikerUpdate.Map(false));
        var placeHighlights = places.Concat(hikerUpdates).Concat(hikerLocationHighlightList);

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

    private static bool IsHikerAtDistance(HikerLocation? hikerLocation, double distance)
    {
        if (hikerLocation == null)
        {
            return false;
        }

        return distance == hikerLocation.Distance;
    }
}
