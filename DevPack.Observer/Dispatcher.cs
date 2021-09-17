using DevPack.Observer.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevPack.Observer
{
    public sealed class Dispatcher<TEvent> : IDispatcher
        where TEvent : IEvent
    {
        private readonly IServiceProvider _serviceProvider;

        public Dispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task SendAsync(object @event)
        {
            var tasks = GetHandleTasks((TEvent)@event).ToArray();

            if (tasks.Length == 1)
                return tasks[0];

            return Task.WhenAll(tasks);
        }

        private IEnumerable<Task> GetHandleTasks(TEvent @event)
        {
            foreach (var handler in _serviceProvider.GetServices<IEventHandler<TEvent>>())
                yield return handler.Handle(@event);
        }
    }
}
