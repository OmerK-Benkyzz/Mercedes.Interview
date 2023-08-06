using MediatR;
using Test.Application.Interfaces.Services;
using Test.Domain.Models;

namespace Test.Application.Commands.ShortenUrl;

public class ShortenUrlCommandHandler : IRequestHandler<ShortenUrlCommand, ApiResponse<ShortenUrlDto>>
{
    private readonly IUrlShortenerService _urlShortenerService;

    public ShortenUrlCommandHandler(IUrlShortenerService urlShortenerService)
    {
        _urlShortenerService = urlShortenerService;
    }

    public async Task<ApiResponse<ShortenUrlDto>> Handle(ShortenUrlCommand request, CancellationToken cancellationToken)
    {
        return _urlShortenerService.ShortenUrl(request.LongUrl, request.CustomAlias);
    }
}