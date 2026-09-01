using System;
using System.Collections.Generic;

namespace Chunity.Utility
{
    public class ObjectPooling<T> where T : class
    {
        private readonly Queue<T> _queue;
        private readonly Func<T> _generateDelegate;
        private readonly Action<T> _resetDelegate;

        public ObjectPooling(Func<T> generate, Action<T> reset)
        {
            _queue = new Queue<T>();
            _generateDelegate = generate;
            _resetDelegate = reset;
        }

        public ObjectPooling(Func<T> generate) : this(generate, null) { }

        public T Dequeue()
        {
            if (_queue.TryDequeue(out var item) == true)
                _resetDelegate?.Invoke(item);
            else
                item = _generateDelegate?.Invoke();

            return item;
        }

        public void Enqueue(T item)
        {
            _queue.Enqueue(item);
        }
    }
}
