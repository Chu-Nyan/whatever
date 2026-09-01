namespace Chunity.GameSystem.AI
{
    public class Selector<T> : BaseContollerNode<T> where T : class
    {
        public override MethodResult Evaluate(T item)
        {
            if (_nodes.Count == 0)
            {
                return MethodResult.Failure;
            }

            foreach (var node in _nodes)
            {
                var result = node.Evaluate(item);

                if (result != MethodResult.Failure)
                    return result;
            }

            return MethodResult.Failure;
        }
    }
}
