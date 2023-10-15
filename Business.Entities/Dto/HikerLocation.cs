namespace Business.Entities.Dto;

public class HikerLocation
{
    public HikerLocation(int id,
        DateTime date,
        bool isManual,
        double lat,
        double lon,
        double? distance,
        int? placeId,
        int? sectionId)
    {
        Id = id;
        Date = date;
        IsManual = isManual;
        Lat = lat;
        Lon = lon;
        Distance = distance;
        PlaceId = placeId;
        SectionId = sectionId;
    }

    public HikerLocation(DateTime date,
        bool isManual,
        double lat,
        double lon,
        double? distance,
        int? placeId,
        int? sectionId)
    {
        Date = date;
        IsManual = isManual;
        Lat = lat;
        Lon = lon;
        Distance = distance;
        PlaceId = placeId;
        SectionId = sectionId;
    }

    public int Id { get; }
    public DateTime Date { get; }
    public bool IsManual { get; }
    public double Lat { get; }
    public double Lon { get; }
    public double? Distance { get; }
    public int? PlaceId { get; }
    public int? SectionId { get; }
}
