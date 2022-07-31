using FluentAssertions;
using FluentAssertions.Primitives;
using System.Net;

namespace Test.Helpers;

public static class ObjectAssertionExtensions
{
    public static async Task<AndWhichConstraint<HttpResponseMessageAssertions, HttpResponseMessage>> BeAValidResponse(this HttpResponseMessageAssertions assertionShould)
    {
        var typed = assertionShould.BeOfType<HttpResponseMessage>("Only response of type HttpResponseMessage can be checked");

        var contentString = await typed.Subject.Content.ReadAsStringAsync();
        typed.Subject.IsSuccessStatusCode.Should().BeTrue($"Response should be successful, but was not. Reason:\r\n{contentString}");

        typed.Subject.StatusCode.Should().NotBe(
            HttpStatusCode.NotFound,
            $"The request could not be made because the endpoint was not found at {typed.Subject.RequestMessage?.RequestUri?.AbsoluteUri}.");

        return typed;
    }

    public static async Task<AndWhichConstraint<HttpResponseMessageAssertions, HttpResponseMessage>> BeEmpty(this HttpResponseMessageAssertions assertionShould)
    {
        var typed = assertionShould.BeOfType<HttpResponseMessage>("Only response of type HttpResponseMessage can be checked");

        var contentString = await typed.Subject.Content.ReadAsStringAsync();
        typed.Subject.IsSuccessStatusCode.Should().BeTrue($"Response should be successful, but was not. Reason:\r\n{contentString}");

        contentString.Should().BeEmpty();

        return typed;
    }
}
