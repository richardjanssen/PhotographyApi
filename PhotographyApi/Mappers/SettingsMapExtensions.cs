using Business.Entities.Dto;
using PhotographyApi.ViewModels.Settings;

namespace PhotographyApi.Mappers;

public static class SettingsMapExtensions
{
    public static SettingsViewModel Map(this Settings settings) => new(settings.TrackingEnabled, settings.MapboxEnabled);

    public static Settings Map(this SettingsViewModel settings) => new(settings.TrackingEnabled, settings.MapboxEnabled);

}
