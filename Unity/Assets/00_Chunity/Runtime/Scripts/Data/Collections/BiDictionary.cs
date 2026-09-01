using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Chunity.Data
{
    public class BiDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _keyToValue;
        private readonly Dictionary<TValue, TKey> _valueToKey;

        public TValue this[TKey key]
        {
            get => _keyToValue[key];
            set
            {
                if (_keyToValue.TryGetValue(key, out var oldValue))
                    _valueToKey.Remove(oldValue);

                if (_valueToKey.TryGetValue(value, out var oldKey))
                    _keyToValue.Remove(oldKey);

                _keyToValue[key] = value;
                _valueToKey[value] = key;
            }
        }

        public ICollection<TKey> Keys
        {
            get => _keyToValue.Keys;
        }

        public ICollection<TValue> Values
        {
            get => _keyToValue.Values;
        }

        public int Count
        {
            get => _keyToValue.Count;
        }

        public bool IsReadOnly
        {
            get => false;
        }

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
        {
            get => _keyToValue.Keys;
        }

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
        {
            get => _keyToValue.Values;
        }

        public BiDictionary()
        {
            _keyToValue = new();
            _valueToKey = new();
        }

        public BiDictionary(int capacity)
        {
            _keyToValue = new(capacity);
            _valueToKey = new(capacity);
        }

        public void Add(TKey key, TValue value)
        {
            if (_keyToValue.ContainsKey(key) == true)
                throw new ArgumentException("Duplicate key.");

            if (_valueToKey.ContainsKey(value) == true)
                throw new ArgumentException("Duplicate value.");

            _keyToValue.Add(key, value);
            _valueToKey.Add(value, key);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _keyToValue.Clear();
            _valueToKey.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _keyToValue.Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            return _keyToValue.ContainsKey(key);
        }

        public bool ContainsValue(TValue value)
        {
            return _valueToKey.ContainsKey(value);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)_keyToValue).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _keyToValue.GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            if (_keyToValue.TryGetValue(key, out var value) == false)
                return false;

            _keyToValue.Remove(key);
            _valueToKey.Remove(value);

            return true;
        }

        public bool Remove(TValue value)
        {
            if (_valueToKey.TryGetValue(value, out var key) == false)
                return false;

            _valueToKey.Remove(value);
            _keyToValue.Remove(key);

            return true;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (Contains(item) == false)
                return false;

            _keyToValue.Remove(item.Key);
            _valueToKey.Remove(item.Value);

            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _keyToValue.TryGetValue(key, out value);
        }

        public bool TryGetKey(TValue value, out TKey key)
        {
            return _valueToKey.TryGetValue(value, out key);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
