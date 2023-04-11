using Business.Entities;
using Business.Interfaces;
using Data.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.Mappers;
using PhotographyApi.ViewModels;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class HighlightController : ControllerBase
{
    private readonly IGetHighlightsQuery _getHighlightsQuery;

    public HighlightController(IGetHighlightsQuery getHighlightsQuery) =>
        _getHighlightsQuery = getHighlightsQuery;

    //[HttpGet]
    //public async Task<IReadOnlyCollection<HighlightViewModel>> GetAll()
    //{
    //    return (await _getHighlightsQuery.Execute()).Select(highlight => highlight.Map()).ToList();
    //}

    [HttpGet]
    public async Task<IReadOnlyCollection<Highlight>> GetAll()
    {
        return (await _getHighlightsQuery.Execute()).ToList();
    }
}