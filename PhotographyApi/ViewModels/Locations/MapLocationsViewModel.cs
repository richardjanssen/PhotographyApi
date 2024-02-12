namespace PhotographyApi.ViewModels.Locations;

public record MapLocationsViewModel(CoordinateViewModel? CurrentLocation, IReadOnlyCollection<CoordinateViewModel> HistoricLocations);
