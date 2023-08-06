using Test.Domain.Entities;

namespace Test.Application.Interfaces.Services;

public interface ILogService
{
    Task AddLogAsync(TransactionLog log);
}