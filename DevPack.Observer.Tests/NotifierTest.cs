using DevPack.Observer.Abstractions;
using DevPack.Tests;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DevPack.Observer.Tests
{
    public class NotifierTest
    {

        [Fact]
        public async Task Notify_SendEventMessageWithSingleHandler_ExpectedReceiveEventMessage()
        {
            //Arrange
            var sp = ServiceProviderHelper.Get(services => services.AddNotifier()
                                                                   .AddEventHandler<CustomEvent, EventHandler>());
            var notifier = sp.GetRequiredService<INotifier>();

            var @event = new CustomEvent("Teste de envio de evento com um handler.");

            //Act
            await notifier.NotifyAsync(@event);

            //Assert
            Assert.NotNull(EventHandler.EventReceived);
            Assert.NotEmpty(EventHandler.EventReceived.Message);
            Assert.Equal(@event.Message, EventHandler.EventReceived.Message);
        }

        [Fact]
        public async Task Notify_SendEventMessageWithMultiplesHandlers_ExpectedReceiveEventsMessages()
        {
            //Arrange
            var sp = ServiceProviderHelper.Get(services =>
                                               services.AddNotifier()
                                                       .AddEventHandler<CustomEvent, MultipleEventHandler>()
                                                       .AddEventHandler<CustomEvent, MultipleEventHandler2>());
            var notifier = sp.GetRequiredService<INotifier>();
            var @event = new CustomEvent("Teste de envio de evento com multiplos handlers.");

            //Act
            await notifier.NotifyAsync(@event);

            //Assert
            Assert.NotNull(MultipleEventHandler.EventReceived);
            Assert.NotEmpty(MultipleEventHandler.EventReceived.Message);
            Assert.Equal(@event.Message, MultipleEventHandler.EventReceived.Message);

            Assert.NotNull(MultipleEventHandler2.EventReceived);
            Assert.NotEmpty(MultipleEventHandler2.EventReceived.Message);
            Assert.Equal(@event.Message, MultipleEventHandler2.EventReceived.Message);
        }

        class CustomEvent : IEvent
        {
            public CustomEvent(string message)
            {
                Message = message;
            }

            public string Message { get; }
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

        class MultipleEventHandler : IEventHandler<CustomEvent>
        {
            public static CustomEvent EventReceived { get; private set; }

            public Task Handle(CustomEvent @event)
            {
                EventReceived = @event;

                return Task.CompletedTask;
            }
        }

        class MultipleEventHandler2 : IEventHandler<CustomEvent>
        {
            public static CustomEvent EventReceived { get; private set; }

            public Task Handle(CustomEvent @event)
            {
                EventReceived = @event;

                return Task.CompletedTask;
            }
        }
    }
}
