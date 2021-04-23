using System.Threading.Tasks;

namespace DevPack
{
    public interface IEventBus
    {
        Task NotifyAsync(object @event);
    }
}