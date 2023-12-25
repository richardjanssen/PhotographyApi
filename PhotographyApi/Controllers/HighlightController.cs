using Business.Interfaces.HighlightsTimeline;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.Mappers;
using PhotographyApi.ViewModels.Highlights;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class HighlightController : ControllerBase
{
    private readonly IGetHighlightsTimelineQuery _getHighlightsQuery;

    public HighlightController(IGetHighlightsTimelineQuery getHighlightsQuery) =>
        _getHighlightsQuery = getHighlightsQuery;

    [HttpGet]
    public async Task<IReadOnlyCollection<HighlightViewModel>> GetAll()
    {
        // Temporary adjustment to trigger error on production
        throw new NotImplementedException();
#pragma warning disable CS0162 // Unreachable code detected
        return (await _getHighlightsQuery.Execute()).Select((highlight) => highlight.Map()).ToList();
#pragma warning restore CS0162 // Unreachable code detected
    }
}