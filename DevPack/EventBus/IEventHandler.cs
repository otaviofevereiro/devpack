using System.Threading.Tasks;

namespace DevPack
{
    public interface IEventHandler<in TEvent>
        where TEvent : IEvent
    {
        public Task Handle(TEvent @event);
    }
}