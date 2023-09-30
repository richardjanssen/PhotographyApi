using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.Mappers;
using PhotographyApi.ViewModels.Places;
using System.Data;

namespace PhotographyApi.Controllers;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class PlaceController : ControllerBase
{
    private readonly IPlacesRepository _placesRepository;

    public PlaceController(IPlacesRepository placesRepository)
    {
        _placesRepository = placesRepository;
    }

    [Authorize(Roles = "PhotographyApi_Admin")]
    [HttpGet]
    public async Task<IReadOnlyCollection<PlaceViewModel>> GetAll()
    {
        return (await _placesRepository.GetPlaces())
            .OrderBy(place => place.Distance)
            .ThenBy(place => place.Title)
            .Select(place => place.Map())
            .ToList();
    }
}
