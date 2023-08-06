using Test.Domain.Entities;
using Test.Infrastructure.Context;
using Test.Infrastructure.Repositories.Interfaces;

namespace Test.Infrastructure.Repositories;

public class TransactionLogRepository : BaseRepository<TransactionLog>, ITransactionLogRepository
{
    public TransactionLogRepository(ApplicationDbContext context) : base(context)
    {
    }


    public async Task<TransactionLog> SaveAsync(TransactionLog log, CancellationToken cancellationToken = default)
    {
        var historyEntry = await DbSet.AddAsync(log, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        return historyEntry.Entity;
    }
}