using Common.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.ViewModels.Weekmenu;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class WeekmenuController() : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = ApplicationRoles.Riesj_ShoppingListEdit)]
    public async Task<WeekmenuDayViewModel[]> Get()
    {
        // Mock data
        return [
            new WeekmenuDayViewModel(3, 1, WeekdayViewModel.Wednesday, "Pasta zalm"),
            new WeekmenuDayViewModel(4, 1, WeekdayViewModel.Thursday, "Nasi"),
            new WeekmenuDayViewModel(5, 1, WeekdayViewModel.Friday, "Kipwraps"),
            new WeekmenuDayViewModel(6, 1, WeekdayViewModel.Saturday, "Kliekje"),
            new WeekmenuDayViewModel(7, 1, WeekdayViewModel.Sunday, "Lasagne"),
            new WeekmenuDayViewModel(8, 1, WeekdayViewModel.Monday, "Kliekje R"),
            new WeekmenuDayViewModel(9, 1, WeekdayViewModel.Tuesday, "Indonesische aubergine")
            ];
    }

    [HttpPost]
    [Authorize(Roles = ApplicationRoles.Riesj_ShoppingListEdit)]
    public async Task Save()
    {
        throw new NotImplementedException();
    }
}