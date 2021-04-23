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
        public static IServiceCollection AddValueFactory<TResult>(this IServiceCollection services,
                                                                 Func<string, TResult> factory)
        {
            services.AddSingleton<IValueFactory<TResult>, ValueFactory<TResult>>(sp => new ValueFactory<TResult>(factory));

            return services;
        }

        public static IServiceCollection AddValueFactory<TResult>(this IServiceCollection services)
        {
            services.AddSingleton<IValueFactory<TResult>, ValueFactory<TResult>>(sp => new ValueFactory<TResult>(key => sp.GetRequiredService<TResult>()));

            return services;
        }

        public static IServiceCollection AddValueFactoryWithServiceProvider<TResult>(this IServiceCollection services,
                                                                 Func<IServiceProvider, TResult> implementationFactory)
        {
            services.AddSingleton<IValueFactory<TResult>, ValueFactory<TResult>>(sp => new ValueFactory<TResult>(key => implementationFactory(sp)));

            return services;
        }
    }
}
