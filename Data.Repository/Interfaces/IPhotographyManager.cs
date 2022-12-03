﻿using Data.Repository.Entities;

namespace Data.Repository.Interfaces;

public interface IPhotographyManager
{
    Task<IReadOnlyCollection<Photo>> GetPhotos();
    Task WritePhotos(IReadOnlyCollection<Photo> photos);
    Task<IReadOnlyCollection<Account>> GetAccounts();
}