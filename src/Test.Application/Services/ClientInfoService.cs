using Microsoft.AspNetCore.Http;
using Test.Application.Interfaces.Services;
using Test.Domain.Dtos;

namespace Test.Application.Services;

public class ClientInfoService : IClientInfoService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClientInfoService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ClientInfoDto GetClientInfo()
    {
        var context = _httpContextAccessor.HttpContext;
        var ip = context.Request.Headers.TryGetValue("X-Forwarded-For", out var header);

        var ipAddress =
            !ip ? context.Connection.RemoteIpAddress?.MapToIPv4().ToString() : header.ToString().Split(",")[0];
        var userAgent = context.Request.Headers["User-Agent"].FirstOrDefault();

        return new ClientInfoDto
        {
            IpAddress = ipAddress,
            UserAgent = userAgent
        };
    }
}