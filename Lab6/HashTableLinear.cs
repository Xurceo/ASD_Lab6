namespace Lab6
{
    public struct KeyValue<K, V>
    {
        public K Key { get; set; }
        public V Value { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class HashTableLinear<K, V> : IHashTable<K, V>
    {
        private readonly int size;
        private readonly KeyValue<K, V>[] items;

        public HashTableLinear(int size)
        {
            this.size = size;
            items = new KeyValue<K, V>[size];
        }

        protected int GetArrayPosition(K key)
        {
            int position = key.GetHashCode() % size;
            return Math.Abs(position);
        }

        public V Find(K key)
        {
            int position = GetArrayPosition(key);
            for (int i = 0; i < size; i++)
            {
                int probePosition = (position + i) % size;
                if (items[probePosition].Key != null && items[probePosition].Key.Equals(key) && !items[probePosition].IsDeleted)
                {
                    return items[probePosition].Value;
                }
            }

            return default(V);
        }

        public void Add(K key, V value)
        {
            int position = GetArrayPosition(key);
            for (int i = 0; i < size; i++)
            {
                int probePosition = (position + i) % size;
                if (items[probePosition].Key == null || items[probePosition].IsDeleted)
                {
                    items[probePosition] = new KeyValue<K, V> { Key = key, Value = value, IsDeleted = false };
                    return;
                }
            }

            throw new InvalidOperationException("Hash table is full");
        }

        public void Remove(K key)
        {
            int position = GetArrayPosition(key);
            for (int i = 0; i < size; i++)
            {
                int probePosition = (position + i) % size;
                if (items[probePosition].Key != null && items[probePosition].Key.Equals(key) && !items[probePosition].IsDeleted)
                {
                    items[probePosition].IsDeleted = true;
                    return;
                }
            }
        }
    }
}
