using DevPack.Tests;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace DevPack.Factory.Tests
{
    public class ValueFactoryTest
    {
        [Fact]
        public void EnsureValue_MultiplesThreads_ThreadSafeExecution()
        {
            //Arrange
            var serviceProvider = ServiceProviderHelper.Get(s => 
                s.AddValueFactory<string, Incrementer>(key => new Incrementer("A")));
            var factory = serviceProvider.GetService<IValueFactory<string, Incrementer>>();

            //Act
            Parallel.For(0, 10, value => factory.GetOrCreate("A").Add());

            //Assert
            var incrementer = factory.GetOrCreate("A");
            Assert.Equal(10, incrementer.Count);
            Assert.Equal("A", incrementer.Name);
        }

        [Fact]
        public void EnsureValue_MultiplesValues_GetCorretValue()
        {
            //Arrange
            var serviceProvider = ServiceProviderHelper.Get(s =>
                s.AddValueFactory<string, Incrementer>(key => new Incrementer(key)));
            var factory = serviceProvider.GetService<IValueFactory<string, Incrementer>>();

            //Act
            Parallel.For(0, 5, value => factory.GetOrCreate("A").Add());
            Parallel.For(0, 4, value => factory.GetOrCreate("B").Add());

            //Assert
            var incrementerA = factory.GetOrCreate("A");
            Assert.Equal(5, incrementerA.Count);
            Assert.Equal("A", incrementerA.Name);

            var incrementerB = factory.GetOrCreate("B");
            Assert.Equal(4, incrementerB.Count);
            Assert.Equal("B", incrementerB.Name);
        }
    }
}
