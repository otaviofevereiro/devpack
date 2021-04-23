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
        private readonly ConcurrentDictionary<string, Lazy<TValue>> _values = new();
        private readonly Func<string, TValue> _create;

        public ValueFactory(Func<string, TValue> factory)
        {
            _create = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public TValue this[string key] => GetOrCreate(key);

        public TValue GetOrCreate(string key)
        {
            return _values.GetOrAdd(key, new Lazy<TValue>(_create(key))).Value;
        }
    }
}
