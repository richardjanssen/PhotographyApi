﻿using Business.Entities.Dto;
using Business.Entities.Highlights;
using Data.Interfaces;

namespace Business.Components.HighlightsTimeline.Internal;

public class GetTimelineHikerLocationsQuery : IGetTimelineHikerLocationsQuery
{
    private readonly IPhotographyRepository _photographyRepository;

    public GetTimelineHikerLocationsQuery(IPhotographyRepository photographyRepository) => _photographyRepository = photographyRepository;

    public async Task<IEnumerable<(PointHighlight Point, int? SectionId)>> Execute(IReadOnlyCollection<Place> places)
    {
        var hikerLocations = (await _photographyRepository.GetHikerLocations()).ToList();
        var mostRecentHikerLocation = hikerLocations.GetMostRecentHikerLocation();
        var hikerLocationsAtPlace = hikerLocations.GetHikerLocationsAtPlace(places);
        var firstLocationPerSection = hikerLocations.GetFirstLocationPerSection();

        return mostRecentHikerLocation.Concat(hikerLocationsAtPlace).Concat(firstLocationPerSection);
    }
}