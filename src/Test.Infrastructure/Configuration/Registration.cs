using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Test.Infrastructure.Configuration.Migrations;
using Test.Infrastructure.Context;
using Test.Infrastructure.Repositories;
using Test.Infrastructure.Repositories.Interfaces;

namespace Test.Infrastructure.Configuration;

public static class Registration
{
    public static IServiceCollection UsePersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .RegisterPostgresql(configuration)
            .RegisterServices();

        return services;
    }

    private static IServiceCollection RegisterPostgresql(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Default"),
                m =>
                {
                    m.MigrationsAssembly(typeof(MigrationProjectAssembly).Assembly.FullName);
                    m.MigrationsHistoryTable("__EFMigrationsHistory", "Migrations");
                    m.EnableRetryOnFailure();
                }
            );
        });
        return services;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ITransactionLogRepository, TransactionLogRepository>();
        // services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        return services;
    }
}