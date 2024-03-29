﻿using Data.Repository.Entities;

namespace Test.Helpers.Builders.Data.Entities;

public class PhotoTestBuilder
{
    private int _id = 1;
    private DateTime _date = TestConstants.SomeDateTime;
    private readonly IReadOnlyCollection<Image> _images = new List<Image>();
    private PhotoTestBuilder() { }

    public static PhotoTestBuilder ATestBuilder() => new();

    public PhotoTestBuilder WithId(int id) => this.With(() => _id = id);

    public PhotoTestBuilder WithDate(DateTime date) => this.With(() => _date = date);

    public Photo Build() => new()
    {
        Id = _id,
        Date = _date,
        Images = _images
    };
}
