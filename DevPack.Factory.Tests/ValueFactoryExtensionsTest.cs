using DevPack.Tests;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DevPack.Factory.Tests
{
    public class ValueFactoryExtensionsTest
    {
        [Fact]
        public void AddValueFactory_AddByFunc_GetCorretService()
        {
            //Arrange
            var serviceProvider = ServiceProviderHelper.Get(s =>
                 s.AddValueFactory<string, Incrementer>(key => new Incrementer(key)));

            //Act
            var factory = serviceProvider.GetService<IValueFactory<string, Incrementer>>();

            //Assert
            Assert.NotNull(factory);
        }

        [Fact]
        public void AddValueFactory_AddByType_GetCorretService()
        {
            //Arrange
            var serviceProvider = ServiceProviderHelper.Get(s =>
                s.AddTransient<Incrementer>()
                 .AddValueFactory<string, Incrementer>());

            //Act
            var factory = serviceProvider.GetService<IValueFactory<string, Incrementer>>();

            //Assert
            Assert.NotNull(factory);
        }

        [Fact]
        public void AddValueFactoryWithServiceProvider_AddByServiceProvider_GetCorretService()
        {
            //Arrange
            var serviceProvider = ServiceProviderHelper.Get(s =>
                s.AddValueFactory<string, Incrementer>(sp => new Incrementer()));

            //Act
            var factory = serviceProvider.GetService<IValueFactory<string, Incrementer>>();

            //Assert
            Assert.NotNull(factory);
        }
    }
}
