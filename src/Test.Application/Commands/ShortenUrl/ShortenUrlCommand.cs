using MediatR;
using Test.Domain.Models;

namespace Test.Application.Commands.ShortenUrl;

public class ShortenUrlCommand : IRequest<ApiResponse<ShortenUrlDto>>
{
    public string LongUrl { get; set; }
    public string? CustomAlias { get; set; }
}