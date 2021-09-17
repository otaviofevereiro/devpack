using System.Threading.Tasks;

namespace DevPack.Observer.Abstractions
{
    public interface IDispatcher
    {
        Task SendAsync(object @event);
    }
}