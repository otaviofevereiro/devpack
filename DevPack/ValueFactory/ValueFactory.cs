using System.Collections.Concurrent;

namespace System
{
    internal sealed class ValueFactory<TResult> : IValueFactory<TResult>
    {
        public static readonly Type _resultType = typeof(TResult);
        private readonly ConcurrentDictionary<string, Lazy<TResult>> _values = new();
        private readonly Func<string, TResult> _create;

        public ValueFactory(Func<string, TResult> factory)
        {
            _create = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public TResult GetOrCreate(string key)
        {
            return _values.GetOrAdd(key, new Lazy<TResult>(_create(key))).Value;
        }
    }
}
