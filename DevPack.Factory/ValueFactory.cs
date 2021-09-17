using System;
using System.Collections.Concurrent;

namespace DevPack.Factory
{
    public sealed class ValueFactory<TKey, TValue> : IValueFactory<TKey, TValue>
    {
        private readonly ConcurrentDictionary<TKey, Lazy<TValue>> _values = new();
        private readonly Func<TKey, TValue> _create;

        public ValueFactory(Func<TKey, TValue> factory)
        {
            _create = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public TValue GetOrCreate(TKey key)
        {
            return _values.GetOrAdd(key, new Lazy<TValue>(_create(key))).Value;
        }
    }
}
