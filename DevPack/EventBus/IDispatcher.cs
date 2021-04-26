using System.Threading.Tasks;

namespace DevPack
{
    internal interface IDispatcher
    {
        Task SendAsync(object @event);
    }
}