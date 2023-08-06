using Test.Application.Interfaces.Services;
using Test.Domain.Entities;
using Test.Infrastructure.Repositories.Interfaces;

namespace Test.Application.Services;

public class LogService : ILogService
{
    private readonly ITransactionLogRepository _logRepository;

    public LogService(ITransactionLogRepository logRepository)
    {
        _logRepository = logRepository;
    }

    public async Task AddLogAsync(TransactionLog log)
    {
        await _logRepository.SaveAsync(log);
    }
}