using MediatR;
using Test.Domain.Models;

namespace Test.Application.Queries.Redirect;

public class RedirectQuery : IRequest<ApiResponse<RedirectUrlDto>>
{
    public string ShortUrl { get; set; }
}