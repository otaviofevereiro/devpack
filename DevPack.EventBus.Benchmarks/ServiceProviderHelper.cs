using Microsoft.Extensions.DependencyInjection;
using System;

namespace DevPack.Observer.Tests.Benchmarks
{
    public static class ServiceProviderHelper
    {
        public static IServiceProvider Get(Action<IServiceCollection> serviceFactory)
        {
            var serviceCollection = new ServiceCollection();

            serviceFactory(serviceCollection);

            return serviceCollection.BuildServiceProvider();
        }
    }
}
