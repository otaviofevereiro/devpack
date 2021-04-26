using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DevPack.EventBus.Benchmarks
{
    [SimpleJob(runtimeMoniker: RuntimeMoniker.NetCoreApp50,
               invocationCount: 1000000)]
    [MemoryDiagnoser]
    [RPlotExporter]
    public class EventBusBenchmark
    {
        private IEventBus _eventBus;

        [GlobalSetup]
        public void Setup()
        {
            //Arrange
            var sp = ServiceProviderHelper.Get(services =>
                                               services.AddEventBus()
                                                       .AddEventHandler<CustomEvent, EventHandler>());
            _eventBus = sp.GetRequiredService<IEventBus>();
        }

        [Benchmark]
        public async Task NotifyAsync()
        {
            var @event = new CustomEvent("Custom Event Value");

            await _eventBus.NotifyAsync(@event);
        }

        class EventHandler : IEventHandler<CustomEvent>
        {
            public static CustomEvent EventReceived { get; private set; }

            public Task Handle(CustomEvent @event)
            {
                EventReceived = @event;

                return Task.CompletedTask;
            }
        }

        class CustomEvent : IEvent
        {
            public CustomEvent(string message)
            {
                Message = message;
            }

            public string Message { get; }
        }
    }
}
