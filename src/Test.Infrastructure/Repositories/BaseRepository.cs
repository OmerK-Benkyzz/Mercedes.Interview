using Microsoft.EntityFrameworkCore;
using Test.Infrastructure.Context;

namespace Test.Infrastructure.Repositories;

public class BaseRepository<T> where T : class
{
    protected readonly ApplicationDbContext Context;
    protected DbSet<T> DbSet;

    protected BaseRepository(ApplicationDbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }
}