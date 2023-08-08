namespace PhotographyApi.ViewModels.Locations;

public record LocationViewModel(int Id,
        DateTime Date,
        bool IsManual,
        double? Lat,
        double? Lon,
        int? PlaceId);
