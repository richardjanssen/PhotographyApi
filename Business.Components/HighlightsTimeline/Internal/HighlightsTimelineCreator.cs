using Business.Entities.Dto;
using Business.Entities.Highlights;

namespace Business.Components.HighlightsTimeline.Internal;

internal static class HighlightsTimelineCreator
{
    public static IReadOnlyCollection<Highlight> CreateTimeline(this List<(PointHighlight Point, Section? Section)> pointsWithSections)
    {
        var highlights = new List<Highlight>();
        var consecutivePointsInSameSection = new List<PointHighlight>();
        Section? previousSection = null;
        Section? currentSection = null;
        for (var i = 0; i < pointsWithSections.Count; i++)
        {
            currentSection = pointsWithSections[i].Section;
            var currentPoint = pointsWithSections[i].Point;
            if (currentSection?.Id == null)
            {
                // Create section highlight with consecutivePointsInSameSection as points if Count > 0, append to highlights
                if (consecutivePointsInSameSection.Count != 0)
                {
                    if (previousSection != null)
                    {
                        highlights.Add(previousSection.Map(consecutivePointsInSameSection));
                    }
                }

                // Create new empty list consecutivePointsInSameSection
                consecutivePointsInSameSection = [];

                // Create point highlight, append to highlights
                highlights.Add(currentPoint.Map());

                // Set previousSection to currentSection
                previousSection = currentSection;
            }
            else if (currentSection?.Id != previousSection?.Id)
            {
                // Create section highlight with consecutivePointsInSameSection as points if Count > 0, append to highlights
                if (consecutivePointsInSameSection.Count != 0)
                {
                    if (previousSection != null)
                    {
                        highlights.Add(previousSection.Map(consecutivePointsInSameSection));
                    }
                }

                // Create new list consecutivePointsInSameSection with current point in it
                consecutivePointsInSameSection = [currentPoint];

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
        if (consecutivePointsInSameSection.Count != 0)
        {
            if (currentSection != null)
            {
                highlights.Add(currentSection.Map(consecutivePointsInSameSection));
            }
        }

        return highlights;
    }
}