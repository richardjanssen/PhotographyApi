using Business.Interfaces.HighlightsTimeline;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.Mappers;
using PhotographyApi.ViewModels.Highlights;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class HighlightController(IGetHighlightsTimelineQuery getHighlightsQuery) : ControllerBase
{
    [HttpGet]
    public async Task<IReadOnlyCollection<HighlightViewModel>> GetAll() =>
        (await getHighlightsQuery.Execute()).Select((highlight) => highlight.Map()).ToList();
}