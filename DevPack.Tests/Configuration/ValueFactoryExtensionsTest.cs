using DevPack.Tests.ValueFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace DevPack.Tests.Configuration
{
    public class ValueFactoryExtensionsTest
    {
        [Fact]
        public void AddValueFactory_AddByFunc_GetCorretService()
        {
            //Arrange
            var serviceProvider = ServiceProviderHelper.Get(s =>
                 s.AddValueFactory(key => new Incrementer(key)));

            //Act
            var factory = serviceProvider.GetService<IValueFactory<Incrementer>>();

            //Assert
            Assert.NotNull(factory);
        }

        [Fact]
        public void AddValueFactory_AddByType_GetCorretService()
        {
            //Arrange
            var serviceProvider = ServiceProviderHelper.Get(s =>
                s.AddTransient<Incrementer>()
                 .AddValueFactory<Incrementer>());

            //Act
            var factory = serviceProvider.GetService<IValueFactory<Incrementer>>();

            //Assert
            Assert.NotNull(factory);
        }

        [Fact]
        public void AddValueFactoryWithServiceProvider_AddByServiceProvider_GetCorretService()
        {
            //Arrange
            var serviceProvider = ServiceProviderHelper.Get(s =>
                s.AddValueFactoryWithServiceProvider(sp => new Incrementer()));

            //Act
            var factory = serviceProvider.GetService<IValueFactory<Incrementer>>();

            //Assert
            Assert.NotNull(factory);
        }
    }
}
