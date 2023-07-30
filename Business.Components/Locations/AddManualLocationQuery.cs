﻿using Business.Entities.Dto;
using Business.Interfaces.Locations;
using Common.Common.Interfaces;
using Data.Interfaces;

namespace Business.Components.Locations;

public class AddManualLocationQuery : IAddManualLocationQuery
{
    private readonly IPhotographyRepository _photographyRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AddManualLocationQuery(IPhotographyRepository photographyRepository, IDateTimeProvider dateTimeProvider)
    {
        _photographyRepository = photographyRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task Execute(int placeId)
    {
        var location = new HikerLocation(_dateTimeProvider.UtcNow, true, null, null, placeId);
        await _photographyRepository.AddHikerLocation(location);
    }
}
