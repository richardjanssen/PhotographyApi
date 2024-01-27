using Business.Components.Locations;
using Business.Components.Locations.Internal;
using Business.Entities;
using Business.Entities.Dto;
using Common.Common.Interfaces;
using Data.Interfaces;
using Data.Proxies.GarminExploreMapShare;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Helpers.Builders.Business.Entities;

namespace Business.Components.Test.Locations;
[TestClass]
public class AddSatelliteMessengerLocationQueryTest
{
    private readonly Mock<IPhotographyRepository> _photographyRepositoryMock = new();
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock = new();
    private readonly Mock<IGarminExploreMapShareManager> _garminExploreMapShareManagerMock = new();
    private readonly Mock<ISettingsRepository> _settingsRepositoryMock = new();
    private readonly Mock<IAddLocationByCoordinateAndDateQuery> _addLocationByCoordinateAndDateQueryMock = new();
    private readonly Mock<ILogger<AddSatelliteMessengerLocationQuery>> _loggerMock = new();

    private readonly double _someLat = 1.23456;
    private readonly double _someLon = 2.34567;
    private readonly double _someOtherLat = 3.45678;
    private readonly double _someOtherLon = 4.56789;
    private readonly DateTime _someDate = new DateTime(2024, 1, 2, 12, 45, 0);

    [TestMethod]
    public async Task Execute_ShouldNotAddLocationWhenTrackingIsDisabled()
    {
        _settingsRepositoryMock.Setup(mock => mock.GetSettings())
            .ReturnsAsync(SettingsTestBuilder.ABuilder().WithTrackingEnabled(false).Build());

        var sut = new AddSatelliteMessengerLocationQuery(
            _photographyRepositoryMock.Object,
            _dateTimeProviderMock.Object,
            _garminExploreMapShareManagerMock.Object,
            _settingsRepositoryMock.Object,
            _addLocationByCoordinateAndDateQueryMock.Object,
            _loggerMock.Object);

        await sut.Execute();

        _addLocationByCoordinateAndDateQueryMock
            .Verify(mock => mock.Execute(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<DateTime>()), Times.Never());
    }

    [DataTestMethod]
    [DataRow(-24.99, false, 0)]
    [DataRow(-25, false, 1)]
    [DataRow(-25.01, false, 1)]
    [DataRow(-24.99, true, 1)]
    [DataRow(-25, true, 1)]
    [DataRow(-25.01, true, 1)]
    public async Task Execute_ShouldAddALocationIfPreviousLocationsSatisfyConditions(
        double previousLocationOffsetFromNow,
        bool previousLocationIsManual,
        int timesAddedLocation)
    {
        _settingsRepositoryMock.Setup(mock => mock.GetSettings())
            .ReturnsAsync(SettingsTestBuilder.ABuilder().WithTrackingEnabled(true).Build());

        var now = new DateTime(2024, 1, 2, 12, 50, 0);
        _dateTimeProviderMock.Setup(mock => mock.UtcNow).Returns(now);

        _photographyRepositoryMock.Setup(mock => mock.GetHikerLocations())
            .ReturnsAsync(new List<HikerLocation>() {
                HikerLocationTestBuilder.ABuilder()
                .WithDate(now.AddMinutes(previousLocationOffsetFromNow))
                .WithIsManual(previousLocationIsManual)
                .WithLat(_someLat)
                .WithLon(_someLon)
                .Build()
            });

        _garminExploreMapShareManagerMock.Setup(mock => mock.GetSatelliteMessengerLocation())
            .ReturnsAsync(SatelliteMessengerLocationTestBuilder.ABuilder()
                .WithLat(_someOtherLat)
                .WithLon(_someOtherLon)
                .WithDate(_someDate)
                .Build());

        var sut = new AddSatelliteMessengerLocationQuery(
            _photographyRepositoryMock.Object,
            _dateTimeProviderMock.Object,
            _garminExploreMapShareManagerMock.Object,
            _settingsRepositoryMock.Object,
            _addLocationByCoordinateAndDateQueryMock.Object,
            _loggerMock.Object);

        await sut.Execute();

        _addLocationByCoordinateAndDateQueryMock
            .Verify(mock => mock.Execute(
                It.IsAny<double>(), It.IsAny<double>(), It.IsAny<DateTime>()), Times.Exactly(timesAddedLocation));
    }

    [TestMethod]
    public async Task Execute_ShouldAddALocationIfThereAreNoPreviousLocations()
    {
        _settingsRepositoryMock.Setup(mock => mock.GetSettings())
            .ReturnsAsync(SettingsTestBuilder.ABuilder().WithTrackingEnabled(true).Build());

        var now = new DateTime(2024, 1, 2, 12, 50, 0);
        _dateTimeProviderMock.Setup(mock => mock.UtcNow).Returns(now);

        _photographyRepositoryMock.Setup(mock => mock.GetHikerLocations()).ReturnsAsync(new List<HikerLocation>());

        _garminExploreMapShareManagerMock.Setup(mock => mock.GetSatelliteMessengerLocation())
            .ReturnsAsync(SatelliteMessengerLocationTestBuilder.ABuilder()
                .WithLat(_someLat)
                .WithLon(_someLon)
                .WithDate(_someDate)
                .Build());

        var sut = new AddSatelliteMessengerLocationQuery(
            _photographyRepositoryMock.Object,
            _dateTimeProviderMock.Object,
            _garminExploreMapShareManagerMock.Object,
            _settingsRepositoryMock.Object,
            _addLocationByCoordinateAndDateQueryMock.Object,
            _loggerMock.Object);

        await sut.Execute();

        _addLocationByCoordinateAndDateQueryMock
            .Verify(mock => mock.Execute(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<DateTime>()), Times.Once());
    }

    [TestMethod]
    public async Task Execute_ShouldNotAddLocationIfThereIsNoSatelliteLocation()
    {
        _settingsRepositoryMock.Setup(mock => mock.GetSettings())
            .ReturnsAsync(SettingsTestBuilder.ABuilder().WithTrackingEnabled(true).Build());

        var now = new DateTime(2024, 1, 2, 12, 50, 0);
        _dateTimeProviderMock.Setup(mock => mock.UtcNow).Returns(now);

        _photographyRepositoryMock.Setup(mock => mock.GetHikerLocations()).ReturnsAsync(new List<HikerLocation>());

        _garminExploreMapShareManagerMock.Setup(mock => mock.GetSatelliteMessengerLocation())
            .ReturnsAsync((SatelliteMessengerLocation?)null);

        var sut = new AddSatelliteMessengerLocationQuery(
            _photographyRepositoryMock.Object,
            _dateTimeProviderMock.Object,
            _garminExploreMapShareManagerMock.Object,
            _settingsRepositoryMock.Object,
            _addLocationByCoordinateAndDateQueryMock.Object,
            _loggerMock.Object);

        await sut.Execute();

        _addLocationByCoordinateAndDateQueryMock
            .Verify(mock => mock.Execute(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<DateTime>()), Times.Never());
    }

    [TestMethod]
    public async Task Execute_ShouldNotAddLocationIfLocationIsAlreadyAdded()
    {
        _settingsRepositoryMock.Setup(mock => mock.GetSettings())
            .ReturnsAsync(SettingsTestBuilder.ABuilder().WithTrackingEnabled(true).Build());

        var now = new DateTime(2024, 1, 2, 12, 50, 0);
        _dateTimeProviderMock.Setup(mock => mock.UtcNow).Returns(now);

        _photographyRepositoryMock.Setup(mock => mock.GetHikerLocations())
            .ReturnsAsync(new List<HikerLocation>() {
                        HikerLocationTestBuilder.ABuilder()
                        .WithDate(_someDate)
                        .WithIsManual(false)
                        .WithLat(_someLat)
                        .WithLon(_someLon)
                        .Build()
            });

        _garminExploreMapShareManagerMock.Setup(mock => mock.GetSatelliteMessengerLocation())
            .ReturnsAsync(SatelliteMessengerLocationTestBuilder.ABuilder()
                .WithLat(_someLat)
                .WithLon(_someLon)
                .WithDate(_someDate)
                .Build());

        var sut = new AddSatelliteMessengerLocationQuery(
            _photographyRepositoryMock.Object,
            _dateTimeProviderMock.Object,
            _garminExploreMapShareManagerMock.Object,
            _settingsRepositoryMock.Object,
            _addLocationByCoordinateAndDateQueryMock.Object,
            _loggerMock.Object);

        await sut.Execute();

        _addLocationByCoordinateAndDateQueryMock
            .Verify(mock => mock.Execute(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<DateTime>()), Times.Never());
    }
}
