using DevPack.Factory;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ValueFactoryExtensions
    {
        public static IServiceCollection AddValueFactory<TKey, TValue>(this IServiceCollection services,
                                                                       Func<TKey, TValue> factory)
        {
            services.AddSingleton<IValueFactory<TKey, TValue>, ValueFactory<TKey, TValue>>(sp =>
                new ValueFactory<TKey, TValue>(factory));

            return services;
        }

        public static IServiceCollection AddValueFactory<TKey, TValue>(this IServiceCollection services)
        {
            services.AddSingleton<IValueFactory<TKey, TValue>, ValueFactory<TKey, TValue>>(sp =>
                new ValueFactory<TKey, TValue>(key => sp.GetRequiredService<TValue>()));

            return services;
        }

        public static IServiceCollection AddValueFactory<TKey, TValue>(this IServiceCollection services,
                                                                      Func<IServiceProvider, TKey, TValue> implementationFactory)
        {
            services.AddSingleton<IValueFactory<TKey, TValue>, ValueFactory<TKey, TValue>>(sp =>
                new ValueFactory<TKey, TValue>(key => implementationFactory(sp, key)));

            return services;
        }
    }
}
