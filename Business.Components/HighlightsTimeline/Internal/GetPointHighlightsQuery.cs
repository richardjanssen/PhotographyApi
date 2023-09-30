using Business.Entities.Highlights;
using Data.Interfaces;

namespace Business.Components.HighlightsTimeline.Internal;

public class GetPointHighlightsQuery : IGetPointHighlightsQuery
{
    private readonly IGetTimelineHikerUpdatesQuery _getTimelineHikerUpdatesQuery;
    private readonly IGetTimelineHikerLocationsQuery _getTimelineHikerLocationsQuery;
    private readonly IPlacesRepository _placesRepository;

    public GetPointHighlightsQuery(
        IGetTimelineHikerUpdatesQuery getTimelineHikerUpdatesQuery,
        IGetTimelineHikerLocationsQuery getTimelineHikerLocationsQuery,
        IPlacesRepository placesRepository)
    {
        _getTimelineHikerUpdatesQuery = getTimelineHikerUpdatesQuery;
        _getTimelineHikerLocationsQuery = getTimelineHikerLocationsQuery;
        _placesRepository = placesRepository;
    }

    public async Task<IEnumerable<(PointHighlight Point, int? SectionId)>> Execute()
    {
        var places = (await _placesRepository.GetPlaces()).ToList();
        var hikerUpdates = await _getTimelineHikerUpdatesQuery.Execute(places);
        var hikerLocations = await _getTimelineHikerLocationsQuery.Execute(places);

        return hikerUpdates.Concat(hikerLocations);
    }
}