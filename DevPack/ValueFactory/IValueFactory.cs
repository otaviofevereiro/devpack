namespace System
{
    public interface IValueFactory<out T>
    {
        T GetOrCreate(string key);
    }
}