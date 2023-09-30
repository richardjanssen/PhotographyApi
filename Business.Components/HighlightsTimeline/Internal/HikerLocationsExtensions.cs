using Business.Entities.Dto;
using Business.Entities.Highlights;

namespace Business.Components.HighlightsTimeline.Internal;

internal static class HikerLocationsExtensions
{
    public static IEnumerable<(PointHighlight Point, int? SectionId)> GetMostRecentHikerLocation(this IReadOnlyCollection<HikerLocation> locations, IReadOnlyCollection<Place> places)
    {
        var mostRecentLocation = locations
            .OrderByDescending(location => location.Date)
            .FirstOrDefault();

        if (mostRecentLocation == null)
        {
            return new List<(PointHighlight Point, int? SectionId)> { };
        }

        var placeOfMostRecentLocation = places
            .Where(place => place.Id == mostRecentLocation.PlaceId)
            .FirstOrDefault();

        var point = new PointHighlight(mostRecentLocation.Id,
            mostRecentLocation.Date,
            Common.Common.Enums.PointHighlightType.location,
            "Riesj is hier",
            placeOfMostRecentLocation?.Distance,
            mostRecentLocation.IsManual);

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