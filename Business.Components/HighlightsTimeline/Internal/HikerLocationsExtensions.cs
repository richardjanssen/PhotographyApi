using Business.Entities.Dto;
using Business.Entities.Highlights;

namespace Business.Components.HighlightsTimeline.Internal;

internal static class HikerLocationsExtensions
{
    public static IEnumerable<(PointHighlight Point, int? SectionId)> GetMostRecentHikerLocation(this IReadOnlyCollection<HikerLocation> locations, IReadOnlyCollection<Place> places)
    {
        var mostRecentLocationWithCoordinates = locations
            .Where(location => location.Lat != null && location.Lon != null)
            .OrderByDescending(location => location.Date)
            .FirstOrDefault();

        if (mostRecentLocationWithCoordinates == null)
        {
            return new List<(PointHighlight Point, int? SectionId)> { };
        }

        var placeOfMostRecentLocation = places
            .Where(place => place.Id == mostRecentLocationWithCoordinates.PlaceId)
            .FirstOrDefault();

        var point = new PointHighlight(mostRecentLocationWithCoordinates.Id,
            mostRecentLocationWithCoordinates.Date,
            Common.Common.Enums.PointHighlightType.location,
            "Riesj is hier",
            placeOfMostRecentLocation?.Distance,
            mostRecentLocationWithCoordinates.IsManual);

        return new List<(PointHighlight Point, int? SectionId)> { (Point: point, placeOfMostRecentLocation?.SectionId) };
    }

    public static IEnumerable<(PointHighlight Point, int? SectionId)> GetHikerLocationsAtPlace(this IReadOnlyCollection<HikerLocation> locations, IReadOnlyCollection<Place> places)
    {
        var hikerLocationsWithPlace = locations
            .Where(hikerLocation => places.Any(place => place.Id == hikerLocation.PlaceId));

        // Remove duplicates in automatic places (we might match the same location multiple times in a row)
        var automaticHikerLocationsWithPlace = hikerLocationsWithPlace.Where(hikerLocation => !hikerLocation.IsManual)
            .OrderBy(hikerLocation => hikerLocation.Date)
            .DistinctBy(hikerLocation => hikerLocation.PlaceId);

        var manualHikerLocationsWithPlace = hikerLocationsWithPlace.Where(hikerLocation => hikerLocation.IsManual);

        return automaticHikerLocationsWithPlace
            .Concat(manualHikerLocationsWithPlace)
            .OrderByDescending(hikerLocation => hikerLocation.Date)
            .Select(hikerLocation =>
            {
                var place = places.First(p => p.Id == hikerLocation.PlaceId);
                var point = new PointHighlight(hikerLocation.Id, hikerLocation.Date, place.Type, place.Title, place.Distance, hikerLocation.IsManual);
                return (Point: point, place.SectionId);
            });
    }
}