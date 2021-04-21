namespace System
{
    public interface IValueFactory<out T>
    {
        T this[string key] { get; }

        T EnsureValue(string key);
    }
}