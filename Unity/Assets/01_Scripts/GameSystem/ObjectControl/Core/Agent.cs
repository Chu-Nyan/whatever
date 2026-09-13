namespace Whatever.ObjectControl
{
    public class Agent
    {
        private IBrainStrategy _behaviorStrategy;
        private int _receiverID;

        private bool _isActivation;

        public IBrainStrategy Brain => _behaviorStrategy;

        public int ReceiverID
        {
            get => _receiverID;
        }

        public bool IsActivation
        {
            get => _isActivation;
        }

        public Agent(IBrainStrategy unitBehavior)
        {
            _behaviorStrategy = unitBehavior;
        }

        public void Tick()
        {
            _behaviorStrategy.Tick();
        }

        public void SetActive(bool isActivation)
        {
            if (isActivation == _isActivation)
                return;

            _isActivation = isActivation;
            if (isActivation == true)
                _behaviorStrategy.Enable();
            else
                _behaviorStrategy.Disable();
        }

        public void Connect(IInputReceiver receiver)
        {
            _receiverID = receiver.ReceiverID;
            _behaviorStrategy.ControlReceiver(receiver);
        }
    }
}
