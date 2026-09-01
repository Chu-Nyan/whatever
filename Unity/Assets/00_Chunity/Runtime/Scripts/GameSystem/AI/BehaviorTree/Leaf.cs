using System;

namespace Chunity.GameSystem.AI
{
    public class Leaf<T> : IBTNode<T> where T : class
    {
        private Func<T, MethodResult> _func;

        public Leaf(Func<T, MethodResult> func)
        {
            _func = func;
        }

        public MethodResult Evaluate(T item)
        {
            if (_func == null)
            {
                return MethodResult.Failure;
            }

            return _func.Invoke(item);
        }
    }
}
