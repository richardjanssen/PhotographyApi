namespace Business.Entities.Locations;

public record MapLocations(Coordinate? CurrentLocation, IReadOnlyCollection<Coordinate> HistoricLocations);
