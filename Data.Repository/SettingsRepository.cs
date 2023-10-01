using Business.Entities.Dto;
using Data.Repository;
using Data.Repository.Interfaces;

namespace Data.Interfaces;

public class SettingsRepository : ISettingsRepository
{
    private readonly IPhotographyManager _photographyManager;

    public SettingsRepository(IPhotographyManager photographyManager) => _photographyManager = photographyManager;

    public async Task<Settings> GetSettings() => (await _photographyManager.GetSettings()).Map();

    public async Task<Settings> UpdateSettings(Settings settings)
    {
        await _photographyManager.WriteSettings(settings.Map());
        return settings;
}
}
