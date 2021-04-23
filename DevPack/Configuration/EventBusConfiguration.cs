using DevPack;
using Microsoft.Extensions.DependencyInjection;

namespace System
{
    public static class EventBusConfiguration
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services)
        {
            return services.AddSingleton<IEventBus, EventBus>();
        }

        public static IServiceCollection AddEventHandler<TEvent, TEventHandler>(this IServiceCollection services)
            where TEventHandler : class, IEventHandler<TEvent>
            where TEvent : IEvent
        {
            services.AddSingleton<Dispatcher<TEvent>>();
            services.AddTransient<IEventHandler<TEvent>, TEventHandler>();

            return services;
        }
    }
}
