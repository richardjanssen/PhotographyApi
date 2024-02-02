using Business.Entities.Dto;
using Business.Entities.Highlights;
using Data.Interfaces;

namespace Business.Components.HighlightsTimeline.Internal;

public class GetTimelineHikerLocationsQuery(IPhotographyRepository photographyRepository) : IGetTimelineHikerLocationsQuery
{
    public async Task<IEnumerable<(PointHighlight Point, int? SectionId)>> Execute(IReadOnlyCollection<Place> places)
    {
        var hikerLocations = (await photographyRepository.GetHikerLocations()).ToList();
        var mostRecentHikerLocation = hikerLocations.GetMostRecentHikerLocation();
        var hikerLocationsAtPlace = hikerLocations.GetHikerLocationsAtPlace(places);
        var firstLocationPerSection = hikerLocations.GetFirstLocationPerSection();

        return mostRecentHikerLocation.Concat(hikerLocationsAtPlace).Concat(firstLocationPerSection);
    }
}