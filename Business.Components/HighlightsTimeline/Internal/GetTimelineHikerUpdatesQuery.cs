using Business.Entities.Dto;
using Business.Entities.Highlights;
using Data.Interfaces;

namespace Business.Components.HighlightsTimeline.Internal;

public class GetTimelineHikerUpdatesQuery : IGetTimelineHikerUpdatesQuery
{
    private readonly IPhotographyRepository _photographyRepository;

    public GetTimelineHikerUpdatesQuery(IPhotographyRepository photographyRepository) =>
        _photographyRepository = photographyRepository;

    public async Task<IEnumerable<(PointHighlight Point, int? SectionId)>> Execute(IReadOnlyCollection<Place> places)
    {
        var hikerUpdates = (await _photographyRepository.GetHikerUpdates()).Select(hikerUpdate =>
        {
            var place = places.FirstOrDefault(p => p.Id == hikerUpdate.PlaceId);
            return (Point: hikerUpdate.Map(), place?.SectionId);
        });

        return hikerUpdates;
    }
}