using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace DevPack
{
    internal sealed class EventBus : IEventBus
    {
        private readonly Type _publisherType = typeof(Dispatcher<>);
        private readonly ConcurrentDictionary<string, Lazy<IDispatcher>> _dispatchers = new();
        private readonly IServiceProvider _serviceProvider;

        public EventBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task NotifyAsync(object @event)
        {
            var eventType = @event.GetType();

            if (!_dispatchers.TryGetValue(eventType.FullName, out Lazy<IDispatcher> dispatcher))
            {
                var eventHandlerType = _publisherType.MakeGenericType(eventType);
                var dispathcher = (IDispatcher)_serviceProvider.GetService(eventHandlerType) ??
                                  throw new ArgumentException($"No event handlers found to event type {eventType.FullName}");

                dispatcher = new Lazy<IDispatcher>(dispathcher);

                _dispatchers.TryAdd(eventType.FullName, dispatcher);
            }

            return dispatcher.Value.SendAsync(@event);
        }
    }
}
