using Test.Domain.Entities;

namespace Test.Infrastructure.Repositories.Interfaces;

public interface ITransactionLogRepository
{
    Task<TransactionLog> SaveAsync(TransactionLog scheduledPayment,
        CancellationToken cancellationToken = default);
}