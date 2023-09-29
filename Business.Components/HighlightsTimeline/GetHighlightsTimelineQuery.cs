using Business.Components.HighlightsTimeline.Internal;
using Business.Entities.Highlights;
using Business.Interfaces.HighlightsTimeline;
using Data.Interfaces;

namespace Business.Components.HighlightsTimeline;

public class GetHighlightsTimelineQuery : IGetHighlightsTimelineQuery
{
    private readonly IPhotographyRepository _photographyRepository;
    private readonly IGetPointHighlightsQuery _getPointHighlightsQuery;

    public GetHighlightsTimelineQuery(IPhotographyRepository photographyRepository, IGetPointHighlightsQuery getPointHighlightsQuery)
    {
        _photographyRepository = photographyRepository;
        _getPointHighlightsQuery = getPointHighlightsQuery;
    }

    public async Task<IReadOnlyCollection<Highlight>> Execute()
    {
        var pointHighlightsTask = _getPointHighlightsQuery.Execute();
        var sections = (await _photographyRepository.GetSections()).OrderBy(section => section.StartDistance).ToList();

        return (await pointHighlightsTask)
            .OrderByDescending(x => x.Point.Date)
            .Select(x => (x.Point, Section: sections.FirstOrDefault(section => section.Id == x.SectionId)))
            .ToList()
            .CreateTimeline();
    }
}