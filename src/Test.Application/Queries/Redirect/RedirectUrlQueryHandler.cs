using MediatR;
using Test.Application.Interfaces.Services;
using Test.Domain.Models;

namespace Test.Application.Queries.Redirect;

public class RedirectQueryHandler : IRequestHandler<RedirectQuery, ApiResponse<RedirectUrlDto>>
{
    private readonly IUrlShortenerService _urlShortenerService;

    public RedirectQueryHandler(IUrlShortenerService urlShortenerService)
    {
        _urlShortenerService = urlShortenerService;
    }

    public async Task<ApiResponse<RedirectUrlDto>> Handle(RedirectQuery request, CancellationToken cancellationToken)
    {
        return _urlShortenerService.Redirect(request.ShortUrl);
    }
}