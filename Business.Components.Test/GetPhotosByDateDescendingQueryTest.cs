using Business.Entities;
using Data.Repository.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using Test.Helpers;
using Test.Helpers.Builders;

namespace Business.Components.Test;

[TestClass]
public class GetPhotosByDateDescendingQueryTest
{
    private readonly Mock<IPhotographyRepository> _photographyRepository = new();

    private readonly DateTime _someDate = TestConstants.SomeDateTime;
    private readonly DateTime _someLaterDate = TestConstants.SomeDateTime.AddDays(1);

    [TestMethod]
    public void Execute_ShouldSortResultsByDescendingDate()
    {
        _photographyRepository.Setup(mock => mock.GetPhotos()).Returns(new List<Photo>()
        {
            PhotoTestBuilder.ATestBuilder().WithId(1).WithDate(_someDate).Build(),
            PhotoTestBuilder.ATestBuilder().WithId(2).WithDate(_someLaterDate).Build(),
        });

        var sut = new GetPhotosByDateDescendingQuery(_photographyRepository.Object);

        sut.Execute().Should().SatisfyRespectively(
            first => first.Date.Should().Be(_someLaterDate),
            second => second.Date.Should().Be(_someDate));
    }
}
