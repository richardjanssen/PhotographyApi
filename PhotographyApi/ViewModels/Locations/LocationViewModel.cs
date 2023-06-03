namespace PhotographyApi.ViewModels.Locations;

public record LocationViewModel(int Id,
        DateTime Date,
        double? ActualDistance,
        double? RoundedDistance,
        bool IsManual,
        double? Lat,
        double? Lon);


