namespace Whatever.ObjectControl
{
    public interface IBrainStrategy
    {
        public BrainType BrainType { get; }

        public void Enable();
        public void Disable();
        public void ControlReceiver(IInputReceiver obj);
        public void Tick();
    }
}
