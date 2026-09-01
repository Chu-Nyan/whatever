using System.Collections.Generic;

namespace Chunity.GameSystem.AI
{
    public abstract class BaseContollerNode<T> : IBTNode<T> where T : class
    {
        protected List<IBTNode<T>> _nodes = new();

        public void AddNode(IBTNode<T> node)
        {
            _nodes.Add(node);
        }

        public abstract MethodResult Evaluate(T item);
    }
}
