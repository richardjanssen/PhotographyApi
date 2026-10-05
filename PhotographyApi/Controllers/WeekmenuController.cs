using Common.Common;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.Mappers.Weekmenu;
using PhotographyApi.ViewModels.Weekmenu;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class WeekmenuController(IWeekmenuDayRepository weekmenuDayRepository) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = ApplicationRoles.Riesj_ShoppingListEdit)]
    public async Task<IReadOnlyCollection<WeekmenuDayViewModel>> Get()
    {
        return [.. (await weekmenuDayRepository.GetWeekmenuDays()).Select(weekmenuDay => weekmenuDay.Map())];
    }

    [HttpPost]
    [Authorize(Roles = ApplicationRoles.Riesj_ShoppingListEdit)]
    public async Task Save(WeekmenuDayViewModel[] weekmenuDayViewModels)
    {
        // Base weekmenu order on order of weekmenu days in request
        var weekmenuDays = weekmenuDayViewModels.Select((weekmenuDay, i) => weekmenuDay.Map(i + 1)).ToList();

        await weekmenuDayRepository.UpdateWeekmenuDays(weekmenuDays);
    }
}