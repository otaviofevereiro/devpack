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
                                                                 Func<string, TValue> factory)
        {
            services.AddSingleton<IValueFactory<TValue>, ValueFactory<TValue>>(sp => new ValueFactory<TValue>(factory));

            return services;
        }

        public static IServiceCollection AddValueFactory<TValue>(this IServiceCollection services)
        {
            services.AddSingleton<IValueFactory<TValue>, ValueFactory<TValue>>(sp => new ValueFactory<TValue>(key => sp.GetRequiredService<TValue>()));

            return services;
        }

        public static IServiceCollection AddValueFactoryWithServiceProvider<TValue>(this IServiceCollection services,
                                                                 Func<IServiceProvider, TValue> implementationFactory)
        {
            services.AddSingleton<IValueFactory<TValue>, ValueFactory<TValue>>(sp => new ValueFactory<TValue>(key => implementationFactory(sp)));

            return services;
        }
    }
}
