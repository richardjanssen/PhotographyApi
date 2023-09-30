namespace Business.Entities.Dto;

public class HikerLocation
{
    public HikerLocation(int id,
        DateTime date,
        bool isManual,
        double lat,
        double lon,
        int? placeId)
    {
        Id = id;
        Date = date;
        IsManual = isManual;
        Lat = lat;
        Lon = lon;
        PlaceId = placeId;
    }

    public HikerLocation(DateTime date,
        bool isManual,
        double lat,
        double lon,
        int? placeId)
    {
        Date = date;
        IsManual = isManual;
        Lat = lat;
        Lon = lon;
        PlaceId = placeId;
    }

    public int Id { get; }
    public DateTime Date { get; }
    public bool IsManual { get; }
    public double Lat { get; }
    public double Lon { get; }
    public int? PlaceId { get; }
}
