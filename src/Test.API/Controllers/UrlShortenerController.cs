using MediatR;
using Microsoft.AspNetCore.Mvc;
using Test.Application.Commands.ShortenUrl;
using Test.Application.Queries.Redirect;
using Test.Domain.Models;

namespace Test.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UrlShortenerController : ControllerBase
{
    private readonly IMediator _mediator;

    public UrlShortenerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Shorten")]
    public async Task<ActionResult<ApiResponse<ShortenUrlDto>>> ShortenUrl([FromBody] ShortenUrlCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpGet("Redirect/{shortUrl}")]
    public async Task<ActionResult<ApiResponse<RedirectUrlDto>>> Redirect(string shortUrl)
    {
        return await _mediator.Send(new RedirectQuery { ShortUrl = shortUrl });
    }
}