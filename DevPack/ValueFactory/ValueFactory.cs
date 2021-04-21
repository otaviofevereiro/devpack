using System.Collections.Concurrent;

namespace System
{
    /// <summary>
    /// Fabrica de valores Thread Safe
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    internal sealed class ValueFactory<TValue> : IValueFactory<TValue>
    {
        public static readonly Type _type = typeof(TValue);
        private readonly ConcurrentDictionary<string, Lazy<TValue>> _clients = new();
        private readonly Func<TValue> _create;

        public ValueFactory(Func<TValue> factory)
        {
            _create = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public TValue this[string key] => EnsureValue(key);

        public TValue EnsureValue(string key)
        {
            return _clients.GetOrAdd(key, new Lazy<TValue>(_create)).Value;
        }
    }
}
