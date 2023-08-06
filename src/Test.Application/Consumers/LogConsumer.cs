using System.Text;
using MassTransit;
using Microsoft.Extensions.Logging;
using Test.Application.Interfaces.Services;
using Test.Application.Messages;
using Test.Domain.Entities;

namespace Test.Application.Consumers;

public class LogConsumer : IConsumer<LogMessage>
{
    private readonly ILogService _logService;
    private readonly ILogger<LogConsumer> _logger;

    public LogConsumer(ILogService logService, ILogger<LogConsumer> logger)
    {
        _logService = logService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<LogMessage> context)
    {
        var logMessage = context.Message;

        await _logService.AddLogAsync(new TransactionLog
        {
            CreateTime = DateTime.Now.ToUniversalTime(),
            IpAddress = logMessage.IpAddress,
            DeviceInfo =logMessage.DeviceInfo,
            Request = Encoding.UTF8.GetBytes(logMessage.Request),
            Response = Encoding.UTF8.GetBytes(logMessage.Response),
            StatusCode = logMessage.StatusCode
        });
    }
}