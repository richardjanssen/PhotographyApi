using Business.Entities.Highlights;
using Data.Interfaces;

namespace Business.Components.HighlightsTimeline.Internal;

public class GetPointHighlightsQuery : IGetPointHighlightsQuery
{
    private readonly IPhotographyRepository _photographyRepository;
    private readonly IGetTimelineHikerUpdatesQuery _getTimelineHikerUpdatesQuery;
    private readonly IGetTimelineHikerLocationsQuery _getTimelineHikerLocationsQuery;

    public GetPointHighlightsQuery(
        IPhotographyRepository photographyRepository,
        IGetTimelineHikerUpdatesQuery getTimelineHikerUpdatesQuery,
        IGetTimelineHikerLocationsQuery getTimelineHikerLocationsQuery)
    {
        _photographyRepository = photographyRepository;
        _getTimelineHikerUpdatesQuery = getTimelineHikerUpdatesQuery;
        _getTimelineHikerLocationsQuery = getTimelineHikerLocationsQuery;
    }

    public async Task<IEnumerable<(PointHighlight Point, int? SectionId)>> Execute()
    {
        var places = (await _photographyRepository.GetPlaces()).ToList();
        var hikerUpdates = await _getTimelineHikerUpdatesQuery.Execute(places);
        var hikerLocations = await _getTimelineHikerLocationsQuery.Execute(places);

        return hikerUpdates.Concat(hikerLocations);
    }
}