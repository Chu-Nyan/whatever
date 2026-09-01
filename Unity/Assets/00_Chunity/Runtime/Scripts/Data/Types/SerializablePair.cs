using System;

namespace Chunity.Data
{
    [Serializable]
    public class SerializablePair<T, K>
    {
        public T Key;
        public K Value;

        public SerializablePair() { }

        public SerializablePair(T key, K value)
        {
            Key = key;
            Value = value;
        }
    }
}
