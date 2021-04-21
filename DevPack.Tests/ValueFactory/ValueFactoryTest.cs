using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DevPack.Tests.ValueFactory
{
    public class ValueFactoryTest
    {
        [Fact]
        public void EnsureValue_MultiplesThreads_ThreadSafeExecution()
        {
            //Arrange
            var serviceProvider = ServiceProviderHelper.Get(s => 
                s.AddValueFactory(() => new Incrementer()));
            var factory = serviceProvider.GetService<IValueFactory<Incrementer>>();

            //Act
            Parallel.For(0, 100, value => factory.EnsureValue("A").Add());

            //Assert
            var incrementer = factory.EnsureValue("A");
            Assert.Equal(100, incrementer.Count);
        }

        [Fact]
        public void EnsureValue_MultiplesValues_GetCorretValue()
        {
            //Arrange
            var serviceProvider = ServiceProviderHelper.Get(s =>
                s.AddValueFactory(() => new Incrementer()));
            var factory = serviceProvider.GetService<IValueFactory<Incrementer>>();

            //Act
            Parallel.For(0, 30, value => factory.EnsureValue("A").Add());
            Parallel.For(0, 20, value => factory.EnsureValue("B").Add());

            //Assert
            var incrementerA = factory.EnsureValue("A");
            Assert.Equal(30, incrementerA.Count);

            var incrementerB = factory.EnsureValue("B");
            Assert.Equal(20, incrementerB.Count);
        }


        [Fact]
        public void ArrayKey_MultiplesThreads_ThreadSafeExecution()
        {
            //Arrange
            var serviceProvider = ServiceProviderHelper.Get(s =>
                s.AddValueFactory(() => new Incrementer()));
            var factory = serviceProvider.GetService<IValueFactory<Incrementer>>();

            //Act
            Parallel.For(0, 100, value => factory["A"].Add());

            //Assert
            var incrementer = factory.EnsureValue("A");
            Assert.Equal(100, incrementer.Count);
        }
    }
}
