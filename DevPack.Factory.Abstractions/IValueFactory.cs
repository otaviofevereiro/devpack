namespace DevPack.Factory
{
    public interface IValueFactory<in TKey, out TValue>
    {
        TValue GetOrCreate(TKey key);
    }
}