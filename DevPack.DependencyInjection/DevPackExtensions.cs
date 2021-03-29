using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class DevPackExtensions
    {
        public static IServiceCollection AddDevPack(this IServiceCollection services, Action<Configuration> configuration = null)
        {
            var _configuration = new Configuration();

            configuration?.Invoke(_configuration);

            services.AddSingleton<IDateTimeProvider>(sp=> new DateTimeProvider(_configuration.DateTimeOffset));

            return services;
        }
    }
}
