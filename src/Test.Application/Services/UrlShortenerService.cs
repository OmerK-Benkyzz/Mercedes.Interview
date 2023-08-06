using Test.Application.Commands.ShortenUrl;
using Test.Application.Interfaces.Services;
using Test.Application.Queries.Redirect;
using Test.Domain.Models;

namespace Test.Application.Services;

public class UrlShortenerService : IUrlShortenerService
{
    private static readonly Dictionary<string, string> urlMap = new Dictionary<string, string>();

    public ApiResponse<ShortenUrlDto> ShortenUrl(string longUrl, string customAlias = null)
    {
        //Open this feature depend on what logic u want inside.
        //if (urlMap.Values.Contains(longUrl))
        //{
        //    throw new InvalidOperationException("URL already shortened.");
        //}

        string shortUrl;
        if (!string.IsNullOrEmpty(customAlias))
        {
            if (urlMap.ContainsKey(customAlias))
            {
                return new ApiResponse<ShortenUrlDto>
                {
                    Response = null,
                    Error = "Custom alias already in use.",
                    StatusCode = 400
                };
            }

            shortUrl = customAlias;
        }
        else
        {
            shortUrl = GenerateShortUrl();
        }

        urlMap.Add(shortUrl, longUrl);

        return new ApiResponse<ShortenUrlDto>
        {
            Response = new ShortenUrlDto
            {
                ShortenUrl = $"http://sample.site/{shortUrl}/"
            },
            Error = null,
            StatusCode = 200
        };
    }

    public ApiResponse<RedirectUrlDto> Redirect(string shortUrl)
    {
        if (urlMap.TryGetValue(shortUrl, out var longUrl))
        {
            return new ApiResponse<RedirectUrlDto>
            {
                Response = new RedirectUrlDto
                {
                    LongUrl = longUrl
                },
                Error = null,
                StatusCode = 200
            };
        }

        return new ApiResponse<RedirectUrlDto>
        {
            Response = null,
            Error = "Redirect Url Not Found",
            StatusCode = 404
        };
    }

    private string GenerateShortUrl()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var shortUrl = new string(Enumerable.Repeat(chars, 6)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        return shortUrl;
    }
}