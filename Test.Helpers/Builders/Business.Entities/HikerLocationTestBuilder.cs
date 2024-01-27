using Business.Entities.Dto;

namespace Test.Helpers.Builders.Business.Entities;

public class HikerLocationTestBuilder
{
    private readonly int _id = 10;
    private DateTime _date = new DateTime(2024, 1, 2, 12, 0, 0);
    private bool _isManual = false;
    private double _lat = 1.23456;
    private double _lon = 2.34567;

    private HikerLocationTestBuilder() { }

    public static HikerLocationTestBuilder ABuilder() => new();

    public HikerLocationTestBuilder WithDate(DateTime date) => this.With(() => _date = date);

    public HikerLocationTestBuilder WithIsManual(bool isManual) => this.With(() => _isManual = isManual);

    public HikerLocationTestBuilder WithLat(double lat) => this.With(() => _lat = lat);

    public HikerLocationTestBuilder WithLon(double lon) => this.With(() => _lon = lon);

    public HikerLocation Build() => new(_id, _date, _isManual, _lat, _lon, null, null, null);
}
