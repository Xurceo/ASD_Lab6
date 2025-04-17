namespace Lab6
{
    public interface IHashTable<K, V>
    {
        void Add(K key, V value);
        V Find(K key);
        void Remove(K key);
    }
}