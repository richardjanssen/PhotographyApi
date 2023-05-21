using Business.Interfaces.GetHighlights;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.Mappers;
using PhotographyApi.ViewModels.Highlights;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class HighlightController : ControllerBase
{
    private readonly IGetHighlightsQuery _getHighlightsQuery;

    public HighlightController(IGetHighlightsQuery getHighlightsQuery) =>
        _getHighlightsQuery = getHighlightsQuery;

    [HttpGet]
    public async Task<IReadOnlyCollection<HighlightViewModel>> GetAll() =>
        (await _getHighlightsQuery.Execute()).Select((highlight, index) => highlight.Map(index)).ToList();
}