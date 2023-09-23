using Business.Entities.Dto;
using Business.Entities.Highlights;
using Business.Interfaces.GetHighlights;
using Data.Interfaces;

namespace Business.Components.GetHighlights;

public class GetHighlightsQuery : IGetHighlightsQuery
{
    private readonly IPhotographyRepository _photographyRepository;

    public GetHighlightsQuery(IPhotographyRepository photographyRepository) =>
        _photographyRepository = photographyRepository;

    public async Task<IReadOnlyCollection<Highlight>> Execute()
    {
        var sectionsTask = _photographyRepository.GetSections();
        var pointsTask = GetPoints();

        var sections = (await sectionsTask).OrderBy(section => section.StartDistance).ToList();
        var points = await pointsTask;

        var pointsWithSections = points
            .OrderByDescending(point => point.Date)
            .Select(point => {
                return (Point: point, Section: GetSectionForPoint(point, sections));
            }).ToList();

        return GetHighlightsTimeline(pointsWithSections);
    }

    private async Task<IEnumerable<PointWithDistance>> GetPoints()
    {
        var placesTask = _photographyRepository.GetPlaces();
        var hikerUpdatesTask = _photographyRepository.GetHikerUpdates();
        var hikerLocationsTask = _photographyRepository.GetHikerLocations();

        var places = (await placesTask);
        var hikerUpdates = (await hikerUpdatesTask).Select(hikerUpdate =>
        {
            var place = places.FirstOrDefault(p => p.Id == hikerUpdate.PlaceId);
            return hikerUpdate.Map(place?.SectionId);
        });
        var hikerLocations = await hikerLocationsTask;

        // Get most recent location 
        var mostRecentLocationWithCoordinates = hikerLocations
            .Where(location => location.Lat != null && location.Lon != null)
            .OrderByDescending(location => location.Date).FirstOrDefault();

        PointWithDistance? mostRecentLocationPoint = null;
        if (mostRecentLocationWithCoordinates != null)
        {
            var placeOfMostRecentLocation = places.Where(place => place.Id == mostRecentLocationWithCoordinates.PlaceId).FirstOrDefault();
            var distance = placeOfMostRecentLocation != null ? placeOfMostRecentLocation.Distance : GetDistanceOnTrail(mostRecentLocationWithCoordinates.Lat, mostRecentLocationWithCoordinates.Lon);
            mostRecentLocationPoint = new PointWithDistance(mostRecentLocationWithCoordinates.Id, placeOfMostRecentLocation?.SectionId, mostRecentLocationWithCoordinates.Date, "Riesj is hier", Common.Common.Enums.PlaceHighlightType.location, distance, mostRecentLocationWithCoordinates.IsManual);
        }

        // Get all hiker locations that match a place
        var hikerLocationsWithPlace = hikerLocations
            .Where(hikerLocation => hikerLocation.PlaceId != null);

        // Remove duplicates in automatic places (we might match the same location multiple times in a row)
        var automaticHikerLocationsWithPlace = hikerLocationsWithPlace.Where(hikerLocation => !hikerLocation.IsManual)
            .OrderBy(hikerLocation => hikerLocation.Date)
            .DistinctBy(hikerLocation => hikerLocation.PlaceId);

        var manualHikerLocationsWithPlace = hikerLocationsWithPlace.Where(hikerLocation => hikerLocation.IsManual);

        var hikerLocationsOrdered = automaticHikerLocationsWithPlace.Concat(manualHikerLocationsWithPlace)
            .OrderByDescending(hikerLocation => hikerLocation.Date)
            .Select(hikerLocation =>
            {
                var place = places.FirstOrDefault(p => p.Id == hikerLocation.PlaceId);
                return place != null
                    ? new PointWithDistance(hikerLocation.Id, place.SectionId, hikerLocation.Date, place.Title, place.Type, place.Distance, hikerLocation.IsManual)
                    : null;
            })
            .OfType<PointWithDistance>();

        return hikerUpdates
            .Concat(hikerLocationsOrdered)
            .Concat(mostRecentLocationPoint != null
            ? new List<PointWithDistance> { mostRecentLocationPoint }
            : new List<PointWithDistance>());
    }

    private static IReadOnlyCollection<Highlight> GetHighlightsTimeline(List<(PointWithDistance Point, Section? Section)> pointsWithSections)
    {
        var highlights = new List<Highlight>();
        var consecutivePointsInSameSection = new List<PointWithDistance>();
        Section? previousSection = null;
        Section? currentSection = null;
        for (var i = 0; i < pointsWithSections.Count; i++)
        {
            currentSection = pointsWithSections[i].Section;
            var currentPoint = pointsWithSections[i].Point;
            if (currentSection?.Id == null)
            {
                // Create section highlight with consecutivePointsInSameSection as points if Count > 0, append to highlights
                if (consecutivePointsInSameSection.Any())
                {
                    if (previousSection != null)
                    {
                        highlights.Add(previousSection.Map(consecutivePointsInSameSection));
                    }
                }

                // Create new empty list consecutivePointsInSameSection
                consecutivePointsInSameSection = new List<PointWithDistance>();

                // Create point highlight, append to highlights
                highlights.Add(currentPoint.Map().Map());

                // Set previousSection to currentSection
                previousSection = currentSection;
            }
            else if (currentSection?.Id != previousSection?.Id)
            {
                // Create section highlight with consecutivePointsInSameSection as points if Count > 0, append to highlights
                if (consecutivePointsInSameSection.Any())
                {
                    if (previousSection != null)
                    {
                        highlights.Add(previousSection.Map(consecutivePointsInSameSection));
                    }
                }

                // Create new list consecutivePointsInSameSection with current point in it
                consecutivePointsInSameSection = new List<PointWithDistance> { currentPoint };

                // Set previousSectionId to currentSectionId
                previousSection = currentSection;
            }
            else
            {
                // Append current point to consecutivePointsInSameSection
                consecutivePointsInSameSection.Add(currentPoint);
            }
        }
        // Create section highlight with consecutivePointsInSameSection as points if Count > 0, append to highlights
        if (consecutivePointsInSameSection.Any())
        {
            if (currentSection != null)
            {
                highlights.Add(currentSection.Map(consecutivePointsInSameSection));
            }
        }

        return highlights;
    }

    private static Section? GetSectionForPoint(PointWithDistance point, IReadOnlyCollection<Section> sections)
    {
        return sections.FirstOrDefault(section => section.Id == point.SectionId);
        //return sections.FirstOrDefault(section => IsPointInSection(point, section));
    }

    //private static bool IsPointInSection(PointWithDistance point, Section section)
    //{
    //    return section.StartDistance <= point.Distance && section.EndDistance > point.Distance;
    //}

    private static double? GetDistanceOnTrail(double? lat, double? lon)
    {
        if (lat == null || lon == null)
        {
            return null;
        }

        // Todo implement some logic here
        return null;
    }
}
