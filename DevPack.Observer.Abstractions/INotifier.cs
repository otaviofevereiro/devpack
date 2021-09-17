using System.Threading.Tasks;

namespace DevPack.Observer.Abstractions
{
    public interface INotifier
    {
        Task NotifyAsync(object @event);
    }
}