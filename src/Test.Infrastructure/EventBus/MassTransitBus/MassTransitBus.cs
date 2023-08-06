using Test.Infrastructure.EventBus.Abstractions;
using MassTransit;

namespace Test.Infrastructure.EventBus.MassTransitBus;

public class MassTransitBus : IEventBus
{
    private readonly IBus _bus;

    public MassTransitBus(IBus bus)
    {
        _bus = bus;
    }

    public Task Publish<T>(T @event) where T : class
    {
        return _bus.Publish(@event);
    }
}