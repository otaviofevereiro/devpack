using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class ValueFactoryExtensions
    {
        public static IServiceCollection AddValueFactory<TValue>(this IServiceCollection services,
                                                                 Func<TValue> factory)
        {
            services.AddSingleton<IValueFactory<TValue>, ValueFactory<TValue>>(sp => new ValueFactory<TValue>(factory));

            return services;
        }

        public static IServiceCollection AddValueFactory<TValue>(this IServiceCollection services)
        {
            services.AddSingleton<IValueFactory<TValue>, ValueFactory<TValue>>(sp => new ValueFactory<TValue>(() => sp.GetRequiredService<TValue>()));

            return services;
        }

        public static IServiceCollection AddValueFactory<TValue>(this IServiceCollection services,
                                                                 Func<IServiceProvider, TValue> implementationFactory)
        {
            services.AddSingleton<IValueFactory<TValue>, ValueFactory<TValue>>(sp => new ValueFactory<TValue>(() => implementationFactory(sp)));

            return services;
        }
    }
}
