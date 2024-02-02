using Business.Entities.Highlights;
using Data.Interfaces;

namespace Business.Components.HighlightsTimeline.Internal;

public class GetPointHighlightsQuery(
    IGetTimelineHikerUpdatesQuery getTimelineHikerUpdatesQuery,
    IGetTimelineHikerLocationsQuery getTimelineHikerLocationsQuery,
    IPlacesRepository placesRepository) : IGetPointHighlightsQuery
{
    public async Task<IEnumerable<(PointHighlight Point, int? SectionId)>> Execute()
    {
        var places = (await placesRepository.GetPlaces()).ToList();
        var hikerUpdates = await getTimelineHikerUpdatesQuery.Execute(places);
        var hikerLocations = await getTimelineHikerLocationsQuery.Execute(places);

        return hikerUpdates.Concat(hikerLocations);
    }
}