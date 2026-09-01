namespace Chunity.GameSystem.AI
{
    public class BehaviorAI<T> where T : class
    {
        private BaseContollerNode<T> _root;
        private bool _isActivation;

        public bool IsActivation
        {
            get => _isActivation;
            set => _isActivation = value;
        }

        public void Init(BaseContollerNode<T> root)
        {
            _root = root;
        }

        public void SetActive(bool isActivation)
        {
            IsActivation = isActivation;
        }

        public void AddRootChild(IBTNode<T> node)
        {
            _root.AddNode(node);
        }

        public void Execute(T item)
        {
            if (_isActivation == false)
                return;

            _root.Evaluate(item);
        }
    }
}
