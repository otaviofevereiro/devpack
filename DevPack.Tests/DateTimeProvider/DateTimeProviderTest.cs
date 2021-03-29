using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DevPack.Tests.DateTimeProvider
{
    public class DateTimeProviderTest
    {
        [Fact]
        public void Now_WithDefaultOffset_ExpectedCorretNowDateTime()
        {
            //Arrange
            var sp = GetServiceProvider();
            var dateTimeProvider = sp.GetRequiredService<IDateTimeProvider>();

            //Act
            var nowResult = dateTimeProvider.Now;

            //Assert
            var now = DateTime.Now;

            Assert.True(now > nowResult);
            Assert.Equal(nowResult.ToString("dd/MM/yyyy hh:mm"), now.ToString("dd/MM/yyyy hh:mm"));
        }

        [Fact]
        public void UtcNow_WithDefaultOffset_ExpectedCorretUtcNowDateTime()
        {
            //Arrange
            var sp = GetServiceProvider();
            var dateTimeProvider = sp.GetRequiredService<IDateTimeProvider>();

            //Act
            var nowResult = dateTimeProvider.UtcNow;

            //Assert
            var now = DateTime.UtcNow;

            Assert.True(now > nowResult);
            Assert.Equal(nowResult.ToString("dd/MM/yyyy hh:mm"), now.ToString("dd/MM/yyyy hh:mm"));
        }

        [Fact]
        public void Now_WithCustomOffset_ExpectedCorretNowDateTime()
        {
            //Arrange
            var sp = GetServiceProvider(offset: TimeSpan.FromHours(10));
            var dateTimeProvider = sp.GetRequiredService<IDateTimeProvider>();

            //Act
            var nowResult = dateTimeProvider.Now;

            //Assert
            var now = DateTime.Now;

            Assert.Equal(nowResult.ToString("dd/MM/yyyy hh:mm"), now.AddHours(13).ToString("dd/MM/yyyy hh:mm"));
        }

        private IServiceProvider GetServiceProvider(TimeSpan offset = default)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDevPack(conf => conf.WithDateTimeOffSet(offset));

            return serviceCollection.BuildServiceProvider();
        }
    }
}
