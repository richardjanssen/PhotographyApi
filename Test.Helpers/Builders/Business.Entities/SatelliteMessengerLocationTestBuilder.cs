using Business.Entities;

namespace Test.Helpers.Builders.Business.Entities;

public class SatelliteMessengerLocationTestBuilder
{
    private double _lat = 1.23456;
    private double _lon = 2.34567;
    private DateTime _date = new DateTime(2024, 1, 2, 12, 0, 0);

    private SatelliteMessengerLocationTestBuilder() { }

    public static SatelliteMessengerLocationTestBuilder ABuilder() => new();

    public SatelliteMessengerLocationTestBuilder WithLat(double lat) => this.With(() => _lat = lat);

    public SatelliteMessengerLocationTestBuilder WithLon(double lon) => this.With(() => _lon = lon);

    public SatelliteMessengerLocationTestBuilder WithDate(DateTime date) => this.With(() => _date = date);

    public SatelliteMessengerLocation Build() => new(_lat, _lon, _date);
}
