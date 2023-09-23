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
    public async Task<IReadOnlyCollection<HighlightViewModel>> GetAll() =>
        (await _getHighlightsQuery.Execute()).Select((highlight) => highlight.Map()).ToList();
}