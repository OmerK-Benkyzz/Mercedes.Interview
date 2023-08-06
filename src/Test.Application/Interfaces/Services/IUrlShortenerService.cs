using Test.Application.Commands.ShortenUrl;
using Test.Application.Queries.Redirect;
using Test.Domain.Models;

namespace Test.Application.Interfaces.Services;

public interface IUrlShortenerService
{
    ApiResponse<ShortenUrlDto> ShortenUrl(string longUrl, string customAlias = null);
    ApiResponse<RedirectUrlDto> Redirect(string shortUrl);
}