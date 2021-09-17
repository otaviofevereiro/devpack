using DevPack;
using DevPack.Observer;
using DevPack.Observer.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace System
{
    public static class NotifierExtensions
    {
        public static IServiceCollection AddNotifier(this IServiceCollection services)
        {
            return services.AddSingleton<INotifier, Notifier>();
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
