using System.Threading.Tasks;

namespace DevPack.Observer.Abstractions
{
    public interface IEventHandler<in TEvent>
        where TEvent : IEvent
    {
        public Task Handle(TEvent @event);
    }
}