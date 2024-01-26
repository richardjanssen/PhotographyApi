using Business.Entities.Dto;

namespace Test.Helpers.Builders.Business.Entities;

public class SettingsTestBuilder
{
    private bool _trackingEnabled = false;
    private bool _mapboxEnabled = false;

    private SettingsTestBuilder() { }

    public static SettingsTestBuilder ABuilder() => new();

    public SettingsTestBuilder WithTrackingEnabled(bool trackingEnabled) => this.With(() => _trackingEnabled = trackingEnabled);

    public SettingsTestBuilder WithMapboxEnabled(bool mapboxEnabled) => this.With(() => _mapboxEnabled = mapboxEnabled);

    public Settings Build() => new(_trackingEnabled, _mapboxEnabled);
}
