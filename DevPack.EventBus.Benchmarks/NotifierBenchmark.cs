using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using DevPack.Observer.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DevPack.Observer.Tests.Benchmarks
{
    [SimpleJob(runtimeMoniker: RuntimeMoniker.NetCoreApp50,
               invocationCount: 1000000)]
    [MemoryDiagnoser]
    [RPlotExporter]
    public class NotifierBenchmark
    {
        private INotifier _notifier;

        [GlobalSetup]
        public void Setup()
        {
            //Arrange
            var sp = ServiceProviderHelper.Get(services =>
                                               services.AddNotifier()
                                                       .AddEventHandler<CustomEvent, EventHandler>());
            _notifier = sp.GetRequiredService<INotifier>();
        }

        [Benchmark]
        public async Task NotifyAsync()
        {
            var @event = new CustomEvent("Custom Event Value");

            await _notifier.NotifyAsync(@event);
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
