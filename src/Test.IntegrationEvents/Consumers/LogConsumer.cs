using System.Runtime.InteropServices.JavaScript;
using MassTransit;
using Newtonsoft.Json;
using Serilog;
using Test.IntegrationEvents.Messages;

namespace Test.IntegrationEvents.Consumers;

// public class LogConsumer : IConsumer<LogMessage>
// {
//     // // private readonly ILogService _logService;
//     //
//     // public LogConsumer(ILogService logService)
//     // {
//     //     _logService = logService;
//     // }
//     //
//     // public Task Consume(ConsumeContext<LogMessage> context)
//     // {
//     //     var logMessage = context.Message;
//     //
//     //
//     //     return Task.CompletedTask;
//     // }
// }