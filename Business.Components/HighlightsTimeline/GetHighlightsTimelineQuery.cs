using Business.Components.HighlightsTimeline.Internal;
using Business.Entities.Highlights;
using Business.Interfaces.HighlightsTimeline;
using Data.Interfaces;

namespace Business.Components.HighlightsTimeline;

public class GetHighlightsTimelineQuery(IPhotographyRepository photographyRepository, IGetPointHighlightsQuery getPointHighlightsQuery) : IGetHighlightsTimelineQuery
{
    public async Task<IReadOnlyCollection<Highlight>> Execute()
    {
        var pointHighlightsTask = getPointHighlightsQuery.Execute();
        var sections = (await photographyRepository.GetSections()).OrderBy(section => section.StartDistance).ToList();

        return (await pointHighlightsTask)
            .OrderByDescending(x => x.Point.Date)
            .Select(x => (x.Point, Section: sections.FirstOrDefault(section => section.Id == x.SectionId)))
            .ToList()
            .CreateTimeline();
    }
}