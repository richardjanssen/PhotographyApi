using Common.Common;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.Mappers;
using PhotographyApi.ViewModels.Settings;

namespace PhotographyApi.Controllers;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class SettingsController : ControllerBase
{
    private readonly ISettingsRepository _settingsRepository;

    public SettingsController(ISettingsRepository settingsRepository) => _settingsRepository = settingsRepository;

    [HttpGet]
    public async Task<SettingsViewModel> Get() =>
        (await _settingsRepository.GetSettings()).Map();

    [HttpPut]
    [Authorize(Roles = ApplicationRoles.Riesj_Admin)]
    public async Task<SettingsViewModel> Update(SettingsViewModel settings) =>
        (await _settingsRepository.UpdateSettings(settings.Map())).Map();

    [HttpGet]
    [Authorize(Roles = ApplicationRoles.Riesj_RecipeEdit)]
    public async Task<string> GetWeekmenuIdeas() =>
    (await _settingsRepository.GetSettings()).WeekmenuIdeas ?? string.Empty;

    [Authorize(Roles = ApplicationRoles.Riesj_RecipeEdit)]
    [HttpPut]
    public async Task UpdateWeekmenuIdeas(UpdateWeekmenuIdeasViewModel vm)
    {
        var currentSettings = await _settingsRepository.GetSettings();
        var newSettings = currentSettings with { WeekmenuIdeas = vm.WeekmenuIdeas };
        await _settingsRepository.UpdateSettings(newSettings);
    }
}
