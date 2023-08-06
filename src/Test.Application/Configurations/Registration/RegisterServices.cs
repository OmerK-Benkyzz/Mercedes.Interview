using System.Reflection;
using AspNetCoreRateLimit;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LogConsumer = Test.Application.Consumers.LogConsumer;

namespace Test.Application.Configurations.Registration;

public static class RegisterServices
{
    public static IServiceCollection UseRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddMassTransitRabbitMq(configuration);
    }

    public static IServiceCollection AddMassTransitRabbitMq(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumers(Assembly.GetExecutingAssembly());
            x.AddConsumer<LogConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                var hostAndPort = $"rabbitmq://{configuration["EventBus:Hostname"]}:{configuration["EventBus:Port"]}";

                cfg.Host(hostAndPort, h =>
                {
                    h.Username(configuration["EventBus:Username"]);
                    h.Password(configuration["EventBus:Password"]);
                });
                cfg.ConfigureEndpoints(context);
            });
        });
        services.Configure<MassTransitHostOptions>(options => { options.WaitUntilStarted = true; });
        return services;
    }

    public static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();
        services.AddMemoryCache();
        services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
        services.AddInMemoryRateLimiting();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        return services;
    }
}