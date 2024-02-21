using Business.Interfaces.HighlightsTimeline;
using Common.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PhotographyApi.Mappers;
using PhotographyApi.ViewModels.Highlights;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class HighlightController(IGetHighlightsTimelineQuery getHighlightsQuery, IMemoryCache memoryCache) : ControllerBase
{
    [HttpGet]
    public async Task<IReadOnlyCollection<HighlightViewModel>> GetAll()
    {
        if (!memoryCache.TryGetValue(CacheKeys.Highlights, out IReadOnlyCollection<HighlightViewModel>? cacheValue))
        {
            cacheValue = (await getHighlightsQuery.Execute()).Select((highlight) => highlight.Map()).ToList();
            memoryCache.Set(CacheKeys.Highlights, cacheValue, TimeSpan.FromMinutes(15));
        }

        return cacheValue ?? [];
    }
}