using Moq;
using Test.Application.Commands.ShortenUrl;
using Test.Application.Interfaces.Services;
using Test.Application.Queries.Redirect;
using Test.Domain.Models;
using Xunit;
using Assert = Xunit.Assert;

namespace Test.UnitTest;

public class UrlShortenerServiceTests
{
    [Fact]
    public async Task RedirectQueryHandler_ShouldReturnRedirectUrl_WhenShortUrlExists()
    {
        // Arrange
        var shortUrl = "shortUrl";
        var longUrl = "http://long.url";
        var serviceMock = new Mock<IUrlShortenerService>();
        serviceMock.Setup(x => x.Redirect(shortUrl)).Returns(new ApiResponse<RedirectUrlDto>
        {
            Response = new RedirectUrlDto
            {
                LongUrl = longUrl
            },
            StatusCode = 200
        });

        var handler = new RedirectQueryHandler(serviceMock.Object);

        // Act
        var result = await handler.Handle(new RedirectQuery { ShortUrl = shortUrl }, default);

        // Assert
        Assert.Equal(longUrl, result.Response.LongUrl);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShortenUrlCommandHandler_ShouldReturnShortUrl_WhenValidLongUrlProvided()
    {
        // Arrange
        var longUrl = "http://long.url";
        var shortUrl = "http://sample.site/shortUrl/";
        var serviceMock = new Mock<IUrlShortenerService>();
        serviceMock.Setup(x => x.ShortenUrl(longUrl, null)).Returns(new ApiResponse<ShortenUrlDto>
        {
            Response = new ShortenUrlDto
            {
                ShortenUrl = shortUrl
            },
            StatusCode = 200
        });

        var handler = new ShortenUrlCommandHandler(serviceMock.Object);

        // Act
        var result = await handler.Handle(new ShortenUrlCommand { LongUrl = longUrl }, default);

        // Assert
        Assert.Equal(shortUrl, result.Response.ShortenUrl);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task RedirectQueryHandler_ShouldReturnNotFound_WhenShortUrlDoesNotExist()
    {
        // Arrange
        var shortUrl = "nonExistingShortUrl";
        var serviceMock = new Mock<IUrlShortenerService>();
        serviceMock.Setup(x => x.Redirect(shortUrl)).Returns(new ApiResponse<RedirectUrlDto>
        {
            Response = null,
            Error = "Redirect Url Not Found",
            StatusCode = 404
        });

        var handler = new RedirectQueryHandler(serviceMock.Object);

        // Act
        var result = await handler.Handle(new RedirectQuery { ShortUrl = shortUrl }, default);

        // Assert
        Assert.Null(result.Response);
        Assert.Equal("Redirect Url Not Found", result.Error);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public async Task ShortenUrlCommandHandler_ShouldReturnError_WhenCustomAliasInUse()
    {
        // Arrange
        var longUrl = "http://long.url";
        var customAlias = "customAlias";
        var serviceMock = new Mock<IUrlShortenerService>();
        serviceMock.Setup(x => x.ShortenUrl(longUrl, customAlias)).Returns(new ApiResponse<ShortenUrlDto>
        {
            Response = null,
            Error = "Custom alias already in use.",
            StatusCode = 400
        });

        var handler = new ShortenUrlCommandHandler(serviceMock.Object);

        // Act
        var result = await handler.Handle(new ShortenUrlCommand { LongUrl = longUrl, CustomAlias = customAlias },
            default);

        // Assert
        Assert.Null(result.Response);
        Assert.Equal("Custom alias already in use.", result.Error);
        Assert.Equal(400, result.StatusCode);
    }
}