namespace Chunity.GameSystem.AI
{
    public class Sequence<T> : BaseContollerNode<T> where T : class
    {
        public override MethodResult Evaluate(T item)
        {
            if (_nodes.Count == 0)
            {
                return MethodResult.Failure;
            }

            for (int i = 0; i < _nodes.Count; i++)
            {
                var result = _nodes[i].Evaluate(item);

                if (result != MethodResult.Success)
                    return result;
            }

            return MethodResult.Success;
        }
    }
}
