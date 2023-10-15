using Business.Components.Authentication;
using Business.Entities.Dto;
using Common.Common;
using Data.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using Test.Helpers.Builders.Business.Entities;

namespace Business.Components.Test;

[TestClass]
public class AuthenticateAccountQueryTest
{
    private readonly Mock<IPhotographyRepository> _photographyRepository = new();
    private readonly Mock<IOptions<AppSettings>> _appSettingsMock = new();

    private readonly string _someUserName = "Elliot";
    private readonly string _somePassword = "VerySecret";
    private readonly string _somePasswordAttempt = "FreshAvocado";
    private readonly string _someSalt = "Pepper";

    [TestMethod]
    public async Task Execute_ShouldReturnNullWhenNoAccountCanBeFound()
    {
        _photographyRepository
            .Setup(mock => mock.GetAccountByUserName(_someUserName))
            .Returns(Task.FromResult<Account?>(null));

        var sut = new AuthenticateAccountQuery(_photographyRepository.Object, _appSettingsMock.Object);

        (await sut.Execute(_someUserName, _somePasswordAttempt)).Should().BeNull();
    }

    [TestMethod]
    public async Task Execute_ShouldReturnNullWhenPasswordIsWrong()
    {
        _photographyRepository
            .Setup(mock => mock.GetAccountByUserName(_someUserName))
            .ReturnsAsync(new Account(_someUserName, _somePassword, _someSalt));
        _appSettingsMock.Setup(mock => mock.Value).Returns(AppSettingsTestBuilder.ATestBuilder().Build());

        var sut = new AuthenticateAccountQuery(_photographyRepository.Object, _appSettingsMock.Object);

        (await sut.Execute(_someUserName, _somePasswordAttempt)).Should().BeNull();
    }
}
