namespace Test.Infrastructure.EventBus.Abstractions;

public interface IEventBus
{
    Task Publish<T>(T @event) where T : class;
}
