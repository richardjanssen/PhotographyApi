using Business.Entities.Dto;

namespace Data.Interfaces;
public interface ISettingsRepository
{
    Task<Settings> GetSettings();
    Task<Settings> UpdateSettings(Settings settings);
}